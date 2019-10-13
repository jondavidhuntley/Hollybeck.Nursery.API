using System.Threading.Tasks;
using Hollybeck.Domain.Entities;
using System.Collections.Generic;
using NurseryServices.Entities.Query;

namespace NurseryServices.Services.DataServices
{
    public interface IPlantDataService
    {
        /// <summary>
        /// Get Plant Detail
        /// </summary>
        /// <param name="plantId">Plant Id</param>
        /// <returns>Query Result</returns>
        Task<PlantQueryResult> GetPlantDetailAsync(int plantId);

        /// <summary>
        /// Get Plant Image Async
        /// </summary>
        /// <param name="plantId">Plant Id</param>
        /// <returns>Plant Image</returns>
        Task<PlantImageQueryResult> GetPlantImageAsync(int plantId);

        /// <summary>
        /// Get All Plants
        /// </summary>
        /// <returns>Plant List</returns>
        Task<List<Plant>> GetAllPlantsAsync();

        /// <summary>
        /// Get Limited Plants
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageNumber">Current Page Number</param>
        /// <returns>Plant List</returns>
        Task<List<Plant>> GetPagedPlantsAsync(int pageSize, int pageNumber);        
    }
}