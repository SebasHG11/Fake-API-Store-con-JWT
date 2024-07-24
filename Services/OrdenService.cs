using System.Security.Claims;
using Api1.Data;
using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Api1.Services{
    public class OrdenService : IOrdenService{
        private readonly ApiContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdenService(ApiContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Orden>> MostrarOrdenes()
        {
            return await _context.Ordens
                .Include(o => o.OrdenProductos)
                .ToListAsync();
        }

        public async Task RealizarOrden(CrearOrdenDTO ordenDTO)
        {
            if(ordenDTO == null || ordenDTO.Productos == null || !ordenDTO.Productos.Any())
            {
                throw new ArgumentException("Los datos de la orden no son validos.");
            }

            try{
                var usuarioActual = GetUsuarioActual();

                if (usuarioActual == null)
                {
                    throw new UnauthorizedAccessException("Usuario no autenticado.");
                }

                var orden = new Orden
                {
                    Fecha = DateTime.Now,
                    UsuarioId = usuarioActual.Id,
                    PrecioTotalCompra = ordenDTO.PrecioTotalCompra,
                    OrdenProductos = ordenDTO?.Productos?.Select(p => new OrdenProducto
                    {
                        ProductoId = p.ProductoId,
                        Cantidad = p.Cantidad
                    }).ToList()
                };

                _context.Ordens.Add(orden);
                await _context.SaveChangesAsync(); 

            }catch(Exception ex){
                throw new InvalidOperationException("Error al realizar la orden", ex);
            }
        }

        public async Task EliminarOrden(int Id)
        {
            var ordenBuscada = _context.Ordens.FirstOrDefault(o => o.Id == Id);

            if(ordenBuscada != null){
                _context.Ordens.Remove(ordenBuscada);
                await _context.SaveChangesAsync();
            }
        }

        public Usuario GetUsuarioActual(){
            var httpContext = _httpContextAccessor.HttpContext;
            var identity = httpContext?.User.Identity as ClaimsIdentity;

            if(identity != null){
                var userClaims = identity.Claims;
                return new Usuario
                {
                    Nombre = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value,
                    Rol = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value,
                    Id = int.Parse(userClaims.FirstOrDefault(u => u.Type == "Id")?.Value)
                };
            }
            return null;
        }
    }

    public interface IOrdenService{
        Task<IEnumerable<Orden>> MostrarOrdenes();
        Task RealizarOrden(CrearOrdenDTO ordenDTO);
        Task EliminarOrden(int Id);
        Usuario GetUsuarioActual();
    }
}