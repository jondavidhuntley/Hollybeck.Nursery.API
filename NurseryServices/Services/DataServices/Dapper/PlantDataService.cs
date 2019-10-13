using Dapper;
using Hollybeck.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NurseryServices.Services.DataServices.Dapper
{
    public class PlantDataService : IPlantDataService
    {
        private readonly string _sqlConnection;

        private ILogger<PlantDataService> _logger;

        public PlantDataService(string sqlConnection, ILogger<PlantDataService> logger)
        {
            _sqlConnection = sqlConnection;
            _logger = logger;
        }

        /// <summary>
        /// Get Plant Detail Async
        /// </summary>
        /// <param name="plantId">Plant Id</param>
        /// <returns>Plant</returns>
        public async Task<Plant> GetPlantDetailAsync(int plantId)
        {
            Plant retVal = null;

            using (var sqlConnection = new SqlConnection(_sqlConnection))
            {
                await sqlConnection.OpenAsync();

                var dynParams = new DynamicParameters();
                dynParams.Add("@PlantId", plantId, System.Data.DbType.Int32);

                retVal = await sqlConnection.QuerySingleOrDefaultAsync<Plant>("usp_Plant_Select_ById", dynParams, commandType: System.Data.CommandType.StoredProcedure);
            }

            return retVal;
        }

        /// <summary>
        /// Get Plant Detail Async
        /// </summary>
        /// <param name="plantId">Plant Id</param>
        /// <returns>Plant</returns>
        public async Task<string> GetPlantDetailAsJsonAsync(int plantId)
        {
            throw new Exception("Not Implemented!");
        }
    }
}
