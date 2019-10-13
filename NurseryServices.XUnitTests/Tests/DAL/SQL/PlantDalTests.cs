using Microsoft.Extensions.Logging;
using Moq;
using NurseryServices.Services.DataServices.DAL.SQL;
using System.Data;
using System.Threading.Tasks;
using Xunit;

namespace NurseryServices.XUnitTests.Tests.DAL.SQL
{
    public class PlantDalTests
    {
        private string _dbConn = string.Empty;

        public PlantDalTests()
        {
            var config = Config.InitConfiguration();
            _dbConn = config["databaseconnection"];
        }

        [Fact]        
        public async Task GetPlantDetailTest()
        {
            int plantId = 107;

            var mockLogger = new Mock<ILogger<PlantDal>>();
                       
            var dalService = new PlantDal(_dbConn, mockLogger.Object);

            DataSet data = await dalService.GetPlantDataById(plantId);

            Assert.NotNull(data);
            Assert.True(data.Tables.Count == 1);
            Assert.True(data.Tables[0].Rows.Count == 1);
            Assert.Equal(data.Tables[0].Rows[0]["PlantId"].ToString(), plantId.ToString());
        }
    }
}