using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse2022.Models;
using ProyectoFinalCoderHouse2022.Repository;

namespace ProyectoFinalCoderHouse2022.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class InicioSesionController : ControllerBase
    {
        [HttpGet("GetNombreApi")]
        public string TraerNombre()
        {
            return "Sistema de Gestion de NicoBrites";
        }
        [HttpGet("InicioSesion")]
        public Usuario InicioSesion(string nombreUsuario, string contraseña)
        {
            return Ado_InicioSesion.InicioSesion(nombreUsuario, contraseña) ;
        }
    }
}
