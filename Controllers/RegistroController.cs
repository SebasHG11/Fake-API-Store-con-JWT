using Api1.Models;
using Api1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers{

    [Route("api/[controller]")]
    public class RegistroController : ControllerBase{
        private readonly IUsuarioService _usuarioService;

        public RegistroController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioDTO usuarioDTO){
            try{
                var usuario = new Usuario
                {
                    Nombre = usuarioDTO.Nombre,
                    Contraseña = usuarioDTO.Contraseña,
                    Rol = "basico",
                    Foto = usuarioDTO.Foto
                };

                await _usuarioService.AgregarUsuario(usuario);
                return Ok( new { message = "Usuario creado correctamente" });
            }catch(Exception ex){
                return BadRequest( new {error = ex.Message} );
            }
        }
    }
}