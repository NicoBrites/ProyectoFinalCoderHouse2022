namespace ProyectoFinalCoderHouse2022.Models
{
    public class ProductoVenta
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }
        public ProductoVenta()
        {
            Id = 0;
            Stock = 0;
            IdProducto = 0;
            IdVenta = 0;
        }
    }
}
