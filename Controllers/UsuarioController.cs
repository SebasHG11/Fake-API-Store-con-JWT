using Api1.Models;
using Api1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase{
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios(){
            try{
               var usuarios = await _usuarioService.MostrarUsuarios();
                return Ok(usuarios); 
            }catch(Exception ex){
                return StatusCode(500, new{ error = ex.Message });
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int Id){
            try{
                var usuario = await _usuarioService.MostrarUsuarioPorId(Id);
                return Ok(usuario);   
            }catch(ArgumentException){
                return NotFound(new{message = "Usuario no encontrado"});
            }catch(Exception ex){
                return StatusCode(500, new{ error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioDTO usuarioDTO){
            try{
                var usuario = new Usuario
                {
                    Nombre = usuarioDTO.Nombre,
                    Contraseña = usuarioDTO.Contraseña,
                    Rol = usuarioDTO.Rol,
                    Foto = usuarioDTO.Foto
                };

                await _usuarioService.AgregarUsuario(usuario);
                return Ok( new { message = "Usuario creado correctamente" });
            }catch(Exception ex){
                return BadRequest( new {error = ex.Message} );
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutUsuario(int Id,[FromBody] UsuarioDTO usuarioDTO){
            try{
                var usuario = await _usuarioService.MostrarUsuarioPorId(Id);

                if(usuario == null){
                    return NotFound(new{ error = "Usuario no encontrado" });
                }

                usuario.Nombre = usuarioDTO.Nombre;
                usuario.Contraseña = usuarioDTO.Contraseña;
                usuario.Rol = usuarioDTO.Rol;
                usuario.Foto = usuarioDTO.Foto;

                await _usuarioService.EditarUsuario(Id, usuario);
                return Ok(new{ message = "Usuario editado correctamente" });
            }catch(Exception ex){
                return BadRequest(new{error = ex.Message});
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUsuario(int Id){
            try{
                await _usuarioService.EliminarUsuario(Id);
                return Ok(new{ message = "Usuario eliminado correctamente" });
            }catch(Exception ex){
                return BadRequest(new{ error = ex.Message });
            }
        }
    }
}