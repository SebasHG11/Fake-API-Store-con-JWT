using Api1.Models;
using Api1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers{
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase{
        private readonly IProductoService _productoService;
        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public IActionResult GetProductos(){
            return Ok(_productoService.MostrarProductos());
        }

        [HttpGet("{id}")]
        public IActionResult GetProductoById(int id){
            var producto = _productoService.MostrarProductoById(id);

            if(producto == null){
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> PostProducto([FromBody] ProductoDTO productoDTO){
            if(productoDTO == null){
                return BadRequest();
            }

            var producto = new Producto{
                Id = productoDTO.Id,
                Nombre = productoDTO.Nombre,
                Precio = productoDTO.Precio,
                IdCategoria = productoDTO.IdCategoria,
                Descripcion = productoDTO.Descripcion,
                Imagen = productoDTO.Imagen
            };

            try
            {
                await _productoService.AgregarProducto(producto);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, [FromBody] ProductoDTO productoDTO){
            if(productoDTO == null){
                return BadRequest();
            }

            var producto = new Producto{
                Id = productoDTO.Id,
                Nombre = productoDTO.Nombre,
                Precio = productoDTO.Precio,
                IdCategoria = productoDTO.IdCategoria,
                Descripcion = productoDTO.Descripcion,
                Imagen = productoDTO.Imagen
            };

            try{
                await _productoService.EditarProducto(id, producto);

                if(producto == null){
                    return NotFound();
                }

                return Ok();
            }catch(Exception ex){
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id){
            try{
                var producto = _productoService.MostrarProductoById(id);
                
                if(producto == null){
                    return NotFound();
                }
                
                await _productoService.EliminarProducto(id);
                return Ok();
            }catch(Exception ex){
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}