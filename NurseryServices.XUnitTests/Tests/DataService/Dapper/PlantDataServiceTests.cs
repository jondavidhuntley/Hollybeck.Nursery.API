using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace NurseryServices.XUnitTests.Tests.DataService.Dapper
{
    public class PlantDataServiceTests
    {
        private string _dbConn = string.Empty;

        public PlantDataServiceTests()
        {
            var config = Config.InitConfiguration();
            _dbConn = config["databaseconnection"];
        }        

        [Fact]
        public async Task GetPlantDetailTest()
        {
            var mockLogger = new Mock<ILogger<PlantDataService>>();            

            var dataService = new PlantDataService(_dbConn, mockLogger.Object);

            var plant = await dataService.GetPlantDetailAsync(107);

            Assert.NotNull(plant);
        }
    }
}