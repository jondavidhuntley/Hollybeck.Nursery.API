using System.Data;
using System.Threading.Tasks;

namespace NurseryServices.Services.DataServices.DAL.SQL
{
    public interface IPlantDal
    {
        Task<DataSet> GetPlantDataById(int plantId);

        /// <summary>
        /// Get DataSer for All Plants
        /// </summary>
        /// <returns>Data Set</returns>
        Task<DataSet> GetAllPlantsData();

        /// <summary>
        /// Get Paged Plant Data
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageNumber">Current Page Number</param>
        /// <returns>Data Set</returns>
        Task<DataSet> GetPagedPlantsData(int pageSize, int pageNumber);

        /// <summary>
        /// Get Plant Image DataSet By Id
        /// </summary>
        /// <param name="plantId">Plant Id</param>
        /// <returns>Data Set</returns>
        Task<DataSet> GetPlantImageDataById(int plantId);
    }
}