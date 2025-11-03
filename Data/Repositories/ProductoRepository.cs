using Microsoft.EntityFrameworkCore;
using Evaluacion.Data.Interfaces;
using Evaluacion.Models;

namespace Evaluacion.Data.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<Producto> CrearAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> ActualizarAsync(int id, Producto producto)
        {
            var existente = await _context.Productos.FindAsync(id);
            if (existente == null) return false;

            // Mapear cambios
            existente.nombre = producto.nombre;
            existente.categoria = producto.categoria;
            existente.precio = producto.precio;

            _context.Productos.Update(existente);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return false;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}