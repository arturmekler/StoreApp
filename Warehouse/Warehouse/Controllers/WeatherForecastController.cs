using Microsoft.AspNetCore.Mvc;
using WarehouseProject.Dto;
using WarehouseProject.Model;

namespace WarehouseProject.Controllers
{
    [ApiController]
    [Route("api")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WarehouseContext _context;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WarehouseContext context)
        {
            _logger = logger;
            _context = context;
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
        [HttpPost]
        [Route("AddWarehouse")]
        public void InsertWarehouse([FromBody] WarehouseDto warehouse)
        {
            var asd = warehouse;
            var qwe = new Warehouse()
            {
                MyProperty = asd.MyProperty,
                Name = asd.Name
            };
            _context.Add(qwe);
            _context.SaveChanges();
        }
        [HttpPost]
        [Route("AddProduct")]
        public void InsertProduct([FromBody] ProductDto warehouse)
        {
            var qwe = new Product()
            {
                Name = warehouse.Name,
                Type = ProductType.RTV,
                Warehouse = _context.Warehouses.Where(el => el.Id == warehouse.WarehouseId).First()
            };
            _context.Add(qwe);
            _context.SaveChanges();
        }
    }
}