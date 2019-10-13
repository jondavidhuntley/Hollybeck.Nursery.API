using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NurseryServices.Services.DataServices.DAL.SQL
{
    public class PlantDal : IPlantDal
    {
        private readonly string _sqlConnection;

        private ILogger<PlantDal> _logger;

        /// <summary>
        /// Plant DAL Constructor
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="logger"></param>
        public PlantDal(string sqlConnection, ILogger<PlantDal> logger)
        {
            _sqlConnection = sqlConnection;
            _logger = logger;
        }

        /// <summary>
        /// Get Plant DataSet By Id
        /// </summary>
        /// <param name="plantId">Plant Id</param>
        /// <returns>Data Set</returns>
        public async Task<DataSet> GetPlantDataById(int plantId)
        {
            DataSet data = null;

            try
            {
                using (var sqlConnection = new SqlConnection(_sqlConnection))
                {
                    await sqlConnection.OpenAsync();

                    SqlCommand sqlCmd = new SqlCommand { Connection = sqlConnection, CommandType = CommandType.StoredProcedure, CommandText = "usp_Plant_Select_ById" };                    

                    SqlParameter sqlParam0 = sqlCmd.Parameters.Add("@PlantId", SqlDbType.Int);
                    sqlParam0.Value = plantId;

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);

                    data = new DataSet();
                    data.Tables.Add("Plant");

                    await Task.Run(() => dataAdapter.Fill(data.Tables[0]));                    
                }
            }
            catch (SqlException dbex)
            {
                _logger.LogInformation(string.Format("Db-Connection : {0}",_sqlConnection));
                _logger.LogError(dbex, "Failed to Get Plant", plantId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Failed to Get Plant Details - Plant Id: {0} : Exception {1}", plantId.ToString(), ex.Message));
                throw;
            }

            return data;
        }

        /// <summary>
        /// Get DataSet for All Plants
        /// </summary>
        /// <returns>Data Set</returns>
        public async Task<DataSet> GetAllPlantsData()
        {
            DataSet data = new DataSet();

            try
            {
                using (var sqlConnection = new SqlConnection(_sqlConnection))
                {
                    await sqlConnection.OpenAsync();

                    SqlCommand sqlCmd = new SqlCommand { Connection = sqlConnection, CommandType = CommandType.StoredProcedure, CommandText = "usp_Plant_Select_All" };
                    
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);

                    await Task.Run(() => dataAdapter.Fill(data));

                    data.Tables[0].TableName = "Plants";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Failed to Get All Plants : Exception {0}", ex.Message));
            }

            return data;
        }

        /// <summary>
        /// Get Paged Plant Data
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageNumber">Current Page Number</param>
        /// <returns>Data Set</returns>
        public async Task<DataSet> GetPagedPlantsData(int pageSize, int pageNumber)
        {
            DataSet data = new DataSet();

            try
            {
                using (var sqlConnection = new SqlConnection(_sqlConnection))
                {
                    await sqlConnection.OpenAsync();

                    SqlCommand sqlCmd = new SqlCommand { Connection = sqlConnection, CommandType = CommandType.StoredProcedure, CommandText = "usp_Plant_Select_Paged" };

                    SqlParameter sqlParam0 = sqlCmd.Parameters.Add("@PageSize", SqlDbType.Int);
                    sqlParam0.Value = pageSize;

                    SqlParameter sqlParam1 = sqlCmd.Parameters.Add("@PageNumber", SqlDbType.Int);
                    sqlParam1.Value = pageNumber;

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);

                    await Task.Run(() => dataAdapter.Fill(data));

                    data.Tables[0].TableName = "Plants";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Failed to Get Paged Plants : Exception {0}", ex.Message));
            }

            return data;
        }

        /// <summary>
        /// Get Plant Image DataSet By Id
        /// </summary>
        /// <param name="plantId">Plant Id</param>
        /// <returns>Data Set</returns>
        public async Task<DataSet> GetPlantImageDataById(int plantId)
        {
            DataSet data = new DataSet();

            try
            {
                using (var sqlConnection = new SqlConnection(_sqlConnection))
                {
                    await sqlConnection.OpenAsync();

                    SqlCommand sqlCmd = new SqlCommand { Connection = sqlConnection, CommandType = CommandType.StoredProcedure, CommandText = "usp_Plant_Image_Select_ById" };

                    SqlParameter sqlParam0 = sqlCmd.Parameters.Add("@PlantId", SqlDbType.Int);
                    sqlParam0.Value = plantId;

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);

                    await Task.Run(() => dataAdapter.Fill(data));

                    data.Tables[0].TableName = "PlantImage";
                }
            }
            catch(SqlException sqlEx)
            {
                _logger.LogError(string.Format("Failed Execute SP {0}", sqlEx.Message));
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Failed to Get Plant Image Details - Plant Id: {0} : Exception {1}", ex.Message));
                throw;
            }

            return data;
        }
    }
}