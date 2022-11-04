using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse2022.Models;
using ProyectoFinalCoderHouse2022.Repository;

namespace ProyectoFinalCoderHouse2022.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {        
        [HttpGet("GetUsuarios")]
        public Usuario Traer(int id)
        {
            return Ado_Usuario.TraerUsuario(id);
        }
        
        [HttpDelete]
        public void Eliminar([FromBody] int id)
        {
            Ado_Usuario.DeleteUsuario(id);

        }

        [HttpPut]
        public void Modificar([FromBody] Usuario usu)
        {
            Ado_Usuario.ModificarUsuario(usu);

        }
        [HttpPost]
        public void Crear([FromBody] Usuario usu)
        {
                Ado_Usuario.CrearUsuario(usu);
        
        }
    }
}
