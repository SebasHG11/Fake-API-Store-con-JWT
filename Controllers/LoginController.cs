using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api1.Data;
using Api1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api1.Controllers{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase{
        private readonly ApiContext _context;
        private readonly IConfiguration _config;

        public LoginController(ApiContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginUser userLogin){
            var user = Authenticate(userLogin);

            if(user != null){
                // Crear token

                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("Usuario no encontrado");
        }

        private Usuario Authenticate(LoginUser userLogin){
            var usuarioActual = _context.Usuarios.FirstOrDefault(user =>
                                                user.Nombre == userLogin.Nombre);

            if(usuarioActual != null && BCrypt.Net.BCrypt.Verify(userLogin.Contraseña, usuarioActual.Contraseña)){
                return usuarioActual;
            }

            return null;
        }

        private string Generate(Usuario user){

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear los claims

            var claims = new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Nombre),
                    new Claim(ClaimTypes.Role, user.Rol)
                };

            //Crear el token

            var token = new JwtSecurityToken(
                            _config["Jwt:Issuer"],
                            _config["Jwt:Audience"],
                            claims,
                            expires: DateTime.Now.AddMinutes(60),
                            signingCredentials: credentials
                        );
                return  new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}