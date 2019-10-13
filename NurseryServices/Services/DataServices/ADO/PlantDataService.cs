using Hollybeck.Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NurseryServices.Entities.Query;
using NurseryServices.Services.Common;
using NurseryServices.Services.DataServices.DAL.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace NurseryServices.Services.DataServices.ADO
{
    public class PlantDataService : IPlantDataService
    {
        private IPlantDal _plantDal;

        private ILogger<PlantDataService> _logger;

        /// <summary>
        /// Plant Data Service
        /// </summary>
        /// <param name="plantDal">Plant DAL</param>
        /// <param name="logger">Logger</param>
        public PlantDataService(IPlantDal plantDal, ILogger<PlantDataService> logger)
        {
            _plantDal = plantDal;
            _logger = logger;
        }

        /// <summary>
        /// Get Plant Detail Async
        /// </summary>
        /// <param name="plantId">Plant Id</param>
        /// <returns>Plant</returns>
        public async Task<PlantQueryResult> GetPlantDetailAsync(int plantId)
        {
            PlantQueryResult retVal = new PlantQueryResult();

            try
            {
                DataSet data = await _plantDal.GetPlantDataById(plantId);

                if (DataUtils.RowsPresent(data))
                {
                    retVal.SelectedPlant = GetPlantItemRow(DataUtils.GetFirstOrDefaultRow(data), true);                    
                }  
                else
                {
                    retVal.NotFound = true;                    
                }

                retVal.Completed = DateTime.Now;
            }
            catch(Exception ex)
            {                
                retVal.FaultMessage = string.Format("FAILED to Recover Plant - ID:{0} - Exception : {1}", plantId.ToString(), ex.Message);
                
                _logger.LogError(retVal.FaultMessage);
            }

            return retVal;
        }

        /// <summary>
        /// Get Plant Image Async
        /// </summary>
        /// <param name="plantId">Plant Id</param>
        /// <returns>Plant Image</returns>
        public async Task<PlantImageQueryResult> GetPlantImageAsync(int plantId)
        {
            PlantImageQueryResult retVal = new PlantImageQueryResult();

            try
            {
                DataSet data = await _plantDal.GetPlantImageDataById(plantId);

                if (DataUtils.RowsPresent(data))
                {
                    retVal.PlantImageDetails = GetImageDetailItemRow(DataUtils.GetFirstOrDefaultRow(data));
                }
                else
                {
                    retVal.NotFound = true;
                }

                retVal.Completed = DateTime.Now;
            }
            catch (Exception ex)
            {
                retVal.FaultMessage = string.Format("Failed to Get Plant Details - Plant Id : {0}, Exception :{1}", plantId.ToString(), ex.Message);

                _logger.LogError(retVal.FaultMessage);
            }

            return retVal;
        }

        /// <summary>
        /// Get All Plants
        /// </summary>
        /// <returns>Plant List</returns>
        public async Task<List<Plant>> GetAllPlantsAsync()
        {
            List<Plant> retVal = null;

            try
            {
                DataSet data = await _plantDal.GetAllPlantsData();

                if (DataUtils.RowsPresent(data))
                {
                    retVal = new List<Plant>();

                    foreach(DataRow row in data.Tables[0].Rows)
                    {
                        Plant item = GetPlantItemRow(row);

                        if (item != null)
                        {
                            retVal.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Failed to Get All Plants, Exception :{0}", ex.Message));
                throw;
            }

            return retVal;
        }

        /// <summary>
        /// Get Limited Plants
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageNumber">Current Page Number</param>
        /// <returns>Plant List</returns>
        public async Task<List<Plant>> GetPagedPlantsAsync(int pageSize, int pageNumber)
        {
            List<Plant> retVal = null;

            try
            {
                DataSet data = await _plantDal.GetPagedPlantsData(pageSize, pageNumber);

                if (DataUtils.RowsPresent(data))
                {
                    retVal = new List<Plant>();

                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        Plant item = GetPlantItemRow(row);

                        if (item != null)
                        {
                            retVal.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Failed to Get Paged Plants Data, Exception :{0}", ex.Message));
                throw;
            }

            return retVal;
        }

            

        /// <summary>
        /// Get Image Detail
        /// </summary>
        /// <param name="row">Data Row</param>
        /// <returns>Image Detail</returns>
        private ImageDetail GetImageDetailItemRow(DataRow row)
        {
            ImageDetail retVal = new ImageDetail
            {
                // BinaryImageFile = (byte[])row["Picture"],
                ImageFilename = DataUtils.DBString(row["ImageFile"])
            };

            if (row.Table.Columns.Contains("Picture"))
            {
                var binaryImage = (byte[])row["Picture"];

                retVal.Base64Image = Convert.ToBase64String(binaryImage);
            }

            if (row.Table.Columns.Contains("LastUpdatedBy"))
            {
                retVal.UpdatedBy = DataUtils.DBString(row["LastUpdatedBy"]);
            }

            if (row.Table.Columns.Contains("LastUpdated"))
            {
                retVal.LastUpdated = DataUtils.DBDateTime(row["LastUpdated"]);
            }

            return retVal;
        }


        /// <summary>
        /// Get Plant Item Row
        /// </summary>
        /// <param name="row">Data Row</param>
        /// <returns>Plant</returns>
        private Plant GetPlantItemRow(DataRow row, bool incImageDetail = false)
        {
            Plant retVal = new Plant
            {
                Id = DataUtils.DBInteger(row["PlantId"]),
                Name = DataUtils.DBString(row["Name"]),
                Size = DataUtils.DBString(row["Size"]),
                Soil = DataUtils.DBString(row["Soil"]),
                Position = DataUtils.DBString(row["Position"]),
                UnitPrice = DataUtils.DBDecimal(row["UnitPrice"]),
                PriceInfo = DataUtils.DBString(row["PriceInfo"]),
                Description = DataUtils.DBString(row["Description"]),
                Reference = DataUtils.DBString(row["Reference"])
            };

            if (row.Table.Columns.Contains("SeasonFrom"))
            {
                retVal.SeasonalStart = DataUtils.DBDateTime(row["SeasonFrom"]);
            }

            if (row.Table.Columns.Contains("SeasonTo"))
            {
                retVal.SeasonalEnd = DataUtils.DBDateTime(row["SeasonTo"]);
            }

            if (row.Table.Columns.Contains("Foliage"))
            {
                retVal.Foliage = DataUtils.DBString(row["Foliage"]);
            }

            if (row.Table.Columns.Contains("LastUpdatedBy"))
            {
                retVal.UpdatedBy = DataUtils.DBString(row["LastUpdatedBy"]);
            }

            if (row.Table.Columns.Contains("LastUpdated"))
            {
                retVal.LastUpdated = DataUtils.DBDateTime(row["LastUpdated"]);
            }

            if (row.Table.Columns.Contains("Picture") && incImageDetail)
            {
                var binaryImage = (byte[])row["Picture"];

                retVal.Base64Image = Convert.ToBase64String(binaryImage);
            }

            if (row.Table.Columns.Contains("ImageFile"))
            {
                retVal.ImageFilename = DataUtils.DBString(row["ImageFile"]);
            }

            if (row.Table.Columns.Contains("PlantOfMonth"))
            {
                retVal.Current = DataUtils.DBBool(row["PlantOfMonth"]);
            }

            if (row.Table.Columns.Contains("NewIn"))
            {
                if (row["NewIn"] != DBNull.Value)
                {
                    if (row["NewIn"].ToString() == "1")
                    {
                        retVal.NewIn = true;
                    }
                }
            }

            if (row.Table.Columns.Contains("RhsAgmAward"))
            {
                if (row["RhsAgmAward"] != DBNull.Value)
                {
                    if (row["RhsAgmAward"].ToString() == "1")
                    {
                        retVal.RhsAgmAward = true;
                    }
                }
            }

            if (row.Table.Columns.Contains("RhsPollinator"))
            {
                if (row["RhsPollinator"] != DBNull.Value)
                {
                    if (row["RhsPollinator"].ToString() == "1")
                    {
                        retVal.RhsPollinator = true;
                    }
                }
            }

            return retVal;
        }
    }
}