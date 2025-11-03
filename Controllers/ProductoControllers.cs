using Microsoft.AspNetCore.Mvc;
using Evaluacion.Data.Interfaces;
using Evaluacion.Models;

namespace Evaluacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoControllers : ControllerBase
    {
        private readonly IProductoRepository _repo;

        public ProductoControllers(IProductoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var productos = await _repo.ObtenerTodosAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var producto = await _repo.ObtenerPorIdAsync(id);
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevo = await _repo.CrearAsync(producto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.id }, nuevo);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Producto producto)
        {
            if (id != producto.id)
                return BadRequest("El ID del producto no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizado = await _repo.ActualizarAsync(producto);
            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _repo.EliminarAsync(id);
            if (!eliminado)
                return NotFound(new { Message = "Producto no encontrado." });

            return NoContent();
        }   
    }
}