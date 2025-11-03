using Microsoft.AspNetCore.Mvc;
using Evaluacion.Data.Interfaces;
using Evaluacion.Models;
using Evaluacion.DTOs;

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
        public async Task<IActionResult> Crear([FromBody] CrearproductoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Mapear DTO a Modelo
            var producto = new Producto
            {
                nombre = dto.Nombre,
                categoria = dto.Categoria,
                precio = dto.Precio
            };

            var nuevo = await _repo.CrearAsync(producto);
            return CreatedAtAction(nameof(ObtenerPorId), new { nuevo.id }, nuevo);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] CrearproductoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Mapear DTO a Modelo
            var producto = new Producto
            {
                nombre = dto.Nombre,
                categoria = dto.Categoria,
                precio = dto.Precio
            };

            var actualizado = await _repo.ActualizarAsync(id, producto);
            if (!actualizado)
                return NotFound(new { mensaje = "Producto no encontrado" });

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