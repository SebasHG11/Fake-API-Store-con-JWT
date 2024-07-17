using Api1.Models;
using Api1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers{
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase{
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public IActionResult GetCategorias(){
            return Ok(_categoriaService.MostrarCategorias());
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoriaById(int id){
            var categoria = _categoriaService.MostrarCategoriaById(id);
            
            if(categoria == null){
                return NotFound();
            }

            return Ok(categoria);
        }

        [HttpGet("{IdCategoria}/productos")]
        public IActionResult GetProductosPorCategoria(int IdCategoria){
            var productos = _categoriaService.MostrarProductosPorCategoria(IdCategoria);

            if(productos == null){
                return BadRequest();
            }

            try{
                return Ok(productos);
            }catch(Exception ex){
                Console.WriteLine(ex);
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostCategoria([FromBody] CategoriaDTO categoriaDTO){
            if(categoriaDTO == null){
                return BadRequest();
            }

            var categoria = new Categoria{
                Id = categoriaDTO.Id,
                Nombre = categoriaDTO.Nombre,
                Imagen = categoriaDTO.Imagen
            };

            try{
                await _categoriaService.AgregarCategoria(categoria);
                return Ok();
            }catch(Exception ex){
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, [FromBody] CategoriaDTO categoriaDTO){
            if(categoriaDTO == null){
                return BadRequest();
            }

            var categoria = new Categoria{
                Id = categoriaDTO.Id,
                Nombre = categoriaDTO.Nombre,
                Imagen = categoriaDTO.Imagen
            };

            try{
                var categoriaExist = _categoriaService.MostrarCategoriaById(id);

                if(categoriaExist == null){
                    return NotFound();
                }

                await _categoriaService.EditarCategoria(id, categoria);
                return Ok();

            }catch(Exception ex){
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id){
            var categoria = _categoriaService.MostrarCategoriaById(id);

            if(categoria == null){
                return NotFound();
            }

            try{
                await _categoriaService.EliminarCategoria(id);
                return Ok();
            }catch(Exception ex){
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}