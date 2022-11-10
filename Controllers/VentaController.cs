using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse2022.Models;
using ProyectoFinalCoderHouse2022.Repository;

namespace ProyectoFinalCoderHouse2022.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public void Crear([FromBody] CrearVenta venta)
        {
            Ado_Venta.CargarVenta(venta);
        }
        [HttpDelete]
        public void Delete([FromBody] int idVenta)
        {
            Ado_Venta.EliminarVenta(idVenta);
        }
        [HttpGet]
        public List<TraerVenta> TraerVenta() // list<TraerVenta>
        {
            return Ado_Venta.TraerVentas();
        }
    }
}
