namespace DataAccess.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductType Type { get; set; }
        public Warehouse Warehouse { get; set;}
    }
}
