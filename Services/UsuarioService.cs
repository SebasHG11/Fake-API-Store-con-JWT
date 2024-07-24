using Api1.Data;
using Api1.Models;
using Microsoft.EntityFrameworkCore;

namespace Api1.Services{
    public class UsuarioService : IUsuarioService{
        private readonly ApiContext _context;

        public UsuarioService(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> MostrarUsuarios(){
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> MostrarUsuarioPorId(int Id){
            var UsuarioBuscado = await _context.Usuarios.FindAsync(Id);

            if(UsuarioBuscado != null){
                return UsuarioBuscado;
            }
            else{
                throw new ArgumentException("Usuario no encontrado");
            }
        }

        public async Task AgregarUsuario(Usuario usuario){
            if(usuario == null){
                throw new ArgumentException("Datos no validos");
            }

            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task EditarUsuario(int Id, Usuario usuario){
            var UsuarioActual = await MostrarUsuarioPorId(Id);

            UsuarioActual.Nombre = usuario.Nombre;
            
            if (!BCrypt.Net.BCrypt.Verify(usuario.Contraseña, UsuarioActual.Contraseña)){
                UsuarioActual.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
            }

            UsuarioActual.Rol = usuario.Rol;
            UsuarioActual.Foto = usuario.Foto;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarUsuario(int Id){
            var UsuarioActual = await MostrarUsuarioPorId(Id);

            _context.Usuarios.Remove(UsuarioActual);
            await _context.SaveChangesAsync();
        }
    }
    public interface IUsuarioService{
        Task<IEnumerable<Usuario>> MostrarUsuarios();
        Task<Usuario> MostrarUsuarioPorId(int Id);
        Task AgregarUsuario(Usuario usuario);
        Task EditarUsuario(int Id, Usuario usuario);
        Task EliminarUsuario(int Id);
    }
}