namespace WarehouseProject.Model
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MyProperty { get; set; }
        public virtual List<Product> Products { get; set; }
     }
}