using Microsoft.AspNetCore.Mvc;
using WarehouseProject.Model;

namespace WarehouseProject.Controllers
{
    [ApiController]
    [Route("api")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Warehouses")]
        public IEnumerable<Model.Warehouse> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Warehouse
            {
                Id = index,
                Name = "Blabla",
                MyProperty = index + 2
            })
            .ToArray();
        }
    }
}