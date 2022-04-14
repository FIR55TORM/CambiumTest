using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoboSquad.Core.Shared.Dtos;
using RoboSquad.Core.Shared.Services;

namespace RoboSquad.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoverController : ControllerBase
    {
        private readonly ILogger<RoverController> _logger;
        private readonly IRoverCommandService _roverCommandService;

        public RoverController(ILogger<RoverController> logger, IRoverCommandService roverCommandService)
        {
            _logger = logger;
            _roverCommandService = roverCommandService;
        }

        //POST /api/rover/executecommands
        [HttpPost]
        [Route("ExecuteCommands")]
        public async Task<IActionResult> ExecuteCommands([FromForm] IFormFile movementsFile, [FromForm] string detailsDtoJson)
        {
            var detailsDto = JsonConvert.DeserializeObject<GridRequestDetailsDto>(detailsDtoJson);

            var result = await _roverCommandService.ReadCommands(movementsFile, detailsDto);

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            var json = JsonConvert.SerializeObject(result, Formatting.Indented, settings);

            return Ok(json);
        }
    }
}
