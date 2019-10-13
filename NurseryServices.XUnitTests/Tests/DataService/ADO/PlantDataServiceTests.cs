using Microsoft.Extensions.Logging;
using Moq;
using NurseryServices.Services.DataServices;
using NurseryServices.Services.DataServices.ADO;
using NurseryServices.Services.DataServices.DAL.SQL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NurseryServices.XUnitTests.Tests.DataService.ADO
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
            int plantId = 107;

            var mockDataServiceLogger = new Mock<ILogger<PlantDataService>>();
            var mockDalLogger = new Mock<ILogger<PlantDal>>();

            IPlantDal plantDal = new PlantDal(_dbConn, mockDalLogger.Object);
            IPlantDataService dataService = new PlantDataService(plantDal, mockDataServiceLogger.Object);

            var plant = await dataService.GetPlantDetailAsync(plantId);

            Assert.NotNull(plant);
            Assert.Equal(plant.SelectedPlant.Id, plantId);
        }        
    }
}
