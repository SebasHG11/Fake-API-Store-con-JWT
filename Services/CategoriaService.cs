using Api1.Data;
using Api1.Models;
using Microsoft.EntityFrameworkCore;

namespace Api1.Services{
    public class CategoriaService : ICategoriaService{
        private readonly ApiContext _context;
        public CategoriaService(ApiContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> MostrarCategorias(){
            return _context.Categorias;
        }

        public Categoria? MostrarCategoriaById(int Id){
            var categoria = _context.Categorias.Find(Id);

            return categoria;
        }

        public IEnumerable<Producto?> MostrarProductosPorCategoria(int IdCategoria){
            return _context.Productos.Where(p => p.IdCategoria == IdCategoria);
        }

        public async Task AgregarCategoria(Categoria categoria){
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarCategoria(int Id){
            var categoria = MostrarCategoriaById(Id);

            if(categoria != null){
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditarCategoria(int Id, Categoria categoria){
            var categoriaEdit = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == Id);

            if(categoriaEdit != null){
                categoriaEdit.Nombre = categoria.Nombre;
                categoriaEdit.Imagen = categoria.Imagen;

                await _context.SaveChangesAsync();
            }
        }
    }
    public interface ICategoriaService{
        IEnumerable<Categoria> MostrarCategorias();
        Categoria? MostrarCategoriaById(int Id);
        IEnumerable<Producto?> MostrarProductosPorCategoria(int id);
        Task AgregarCategoria(Categoria categoria);
        Task EliminarCategoria(int Id);
        Task EditarCategoria(int id, Categoria categoria);
    }
}