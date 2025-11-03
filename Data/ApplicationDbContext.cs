using Microsoft.EntityFrameworkCore;
using Evaluacion.Models;

namespace Evaluacion.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        public DbSet<Producto> Productos { get; set; }
    }
}