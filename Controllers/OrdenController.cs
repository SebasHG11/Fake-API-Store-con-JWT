using Api1.Models;
using Api1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers{
    [Route("api/[controller]")]
    public class OrdenController : ControllerBase{
        private readonly IOrdenService _ordenService;

        public OrdenController(IOrdenService ordenService)
        {
            _ordenService = ordenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdenes(){
            try{
                var ordenes = await _ordenService.MostrarOrdenes();
                return Ok(ordenes);
            }catch(Exception ex){
                return BadRequest(new{error = ex});
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> PostOrden([FromBody] CrearOrdenDTO ordenDTO){
            try{
                await _ordenService.RealizarOrden(ordenDTO);
                return Ok(new { message = "Orden creada correctamente." });  
            }catch(Exception ex){
                return BadRequest(new{ error = ex.Message });
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOrden(int Id){
            try{
                await _ordenService.EliminarOrden(Id);
                return Ok(new { message = "Orden eliminada correctamente" });
            }catch(Exception ex){
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}