using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse2022.Models;
using ProyectoFinalCoderHouse2022.Repository;

namespace ProyectoFinalCoderHouse2022.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet("TraerProductos")]
        public List<Producto> TraerListaProductos()
        {
            return Ado_Producto.TraerProductos();
        }
        [HttpPost]
        public void Crear([FromBody] Producto prod)
        {
            Ado_Producto.CrearProducto(prod);
        }
        [HttpPut]
        public void Modificar([FromBody] Producto prod)
        {
            Ado_Producto.ModificarProducto(prod);
        }
        [HttpDelete]
        public void Eliminar([FromBody] int id)
        {
            Ado_Producto.DeleteProducto(id);
        }
    }
}
