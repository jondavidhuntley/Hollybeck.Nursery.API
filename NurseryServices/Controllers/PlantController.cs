using Hollybeck.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NurseryServices.Entities.Query;
using NurseryServices.Services.DataServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NurseryServices.Controllers
{
    [Route("api/[controller]")]
    public class PlantController : Controller
    {
        /// <summary>
        /// Plant Data Service
        /// </summary>
        private readonly IPlantDataService _plantDataService;

        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<PlantController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plantDataService"></param>
        /// <param name="logger"></param>
        public PlantController(IPlantDataService plantDataService, ILogger<PlantController> logger)
        {
            _plantDataService = plantDataService;
            _logger = logger;
        }      

        [HttpGet]
        [Route("plant/{id}")]
        [ProducesResponseType(typeof(PlantQueryResult), 200)]
        public async Task<IActionResult> GetPlantDetail(int id)
        {           
            _logger.LogInformation("Beginning Plant Details Fetch!");

            var result = await _plantDataService.GetPlantDetailAsync(id);

            if (result.InnerException)
            {
                _logger.LogError(string.Format("Error Fetching Plant with Id: {0} - Exception :{1}!", id.ToString(), result.FaultMessage));
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, result.FaultMessage);
            }
            else
            {
                _logger.LogInformation("Completed Plant Details Fetch!");

                if (result.NotFound)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return this.Ok(result);
                }
            }            
        }        

        [HttpGet]
        [Route("plantimage/{id}")]
        [ProducesResponseType(typeof(PlantImageQueryResult), 200)]
        public async Task<IActionResult> GetPlantImage(int id)
        {
            _logger.LogInformation("Beginning Plant Image Fetch!");

            var result = await _plantDataService.GetPlantImageAsync(id);

            if (result.InnerException)
            {
                _logger.LogError(string.Format("Error Fetching Plant Image with Id: {0} - Exception :{1}", id.ToString(), result.FaultMessage));

                return this.StatusCode(StatusCodes.Status500InternalServerError, result.FaultMessage);
            }
            else
            {
                _logger.LogInformation("Completed Plant Image Fetch!");

                if (result.NotFound)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return this.Ok(result);
                }
            }
        }

        [HttpGet]
        [Route("plants/{pageSize}/{pageNumber}")]
        [ProducesResponseType(typeof(List<Plant>), 200)]
        public async Task<IActionResult> GetPagedPlants(int pageSize, int pageNumber)
        {
            var result = await _plantDataService.GetPagedPlantsAsync(pageSize, pageNumber);

            if (result != null)
            {
                return this.Ok(result);
            }
            else
            {
                return this.StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}