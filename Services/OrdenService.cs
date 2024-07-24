using Api1.Data;
using Api1.Models;
using Microsoft.EntityFrameworkCore;

namespace Api1.Services{
    public class OrdenService : IOrdenService{
        private readonly ApiContext _context;

        public OrdenService(ApiContext context)
        {
            _context = context;
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
                var usuarioId = 2; //Obtener el id del usuario autenticado

                var orden = new Orden
                {
                    Fecha = DateTime.Now,
                    UsuarioId = usuarioId,
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
    }

    public interface IOrdenService{
        Task<IEnumerable<Orden>> MostrarOrdenes();
        Task RealizarOrden(CrearOrdenDTO ordenDTO);
        Task EliminarOrden(int Id);
    }
}