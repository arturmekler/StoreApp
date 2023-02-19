using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseProject.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public ProductTypeDto Type { get; set; }
        public int WarehouseId { get; set; }
    }
}
