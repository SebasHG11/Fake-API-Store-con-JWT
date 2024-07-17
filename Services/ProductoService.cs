using Api1.Data;
using Api1.Models;
using Microsoft.EntityFrameworkCore;

namespace Api1.Services{
    public class ProductoService : IProductoService{
        private readonly ApiContext _context;

        public ProductoService(ApiContext context)
        {
            _context = context;
        }

        public IEnumerable<Producto> MostrarProductos(){
            return _context.Productos.Include(p => p.Categoria);
        }

        public Producto? MostrarProductoById(int Id){
            var producto = _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == Id);
            
            return producto;
        }

        public async Task AgregarProducto(Producto producto){
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarProducto(int Id){
            var producto = MostrarProductoById(Id);

            if(producto != null){
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditarProducto(int Id, Producto producto){
            var productoEdit = await _context.Productos.FindAsync(Id);
            if(productoEdit != null){
                productoEdit.Nombre = producto.Nombre;
                productoEdit.Precio = producto.Precio;
                productoEdit.IdCategoria = producto.IdCategoria;
                productoEdit.Descripcion = producto.Descripcion;
                productoEdit.Imagen = producto.Imagen;

                await _context.SaveChangesAsync();
            }
        }
    }

    public interface IProductoService{
        IEnumerable<Producto> MostrarProductos();
        Producto? MostrarProductoById(int Id);
        Task AgregarProducto(Producto producto);
        Task EliminarProducto(int Id);
        Task EditarProducto(int Id, Producto producto);
    }
}