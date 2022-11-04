using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse2022.Models;
using ProyectoFinalCoderHouse2022.Repository;

namespace ProyectoFinalCoderHouse2022.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ProductoVentaController
    {
        [HttpGet("TraerProductosVendidos")]
        public List<ProductoVenta> TraerProductoVendido(int idUsuario)
        {
            return Ado_ProductoVenta.TraerProductoVendido(idUsuario);
        }
    }
}
