using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse2022.Models;
using ProyectoFinalCoderHouse2022.Repository;

namespace ProyectoFinalCoderHouse2022.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /*[HttpGet("GetListUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            return Ado_Usuario.TraerListaUsuarios();
        }*/
        
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
        public IActionResult Crear([FromBody] Usuario usu)
        {
            try
            {
                Ado_Usuario.CrearUsuario(usu);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

    }
}
