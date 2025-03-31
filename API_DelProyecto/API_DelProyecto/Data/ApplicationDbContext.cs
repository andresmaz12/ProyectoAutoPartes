using Microsoft.EntityFrameworkCore;
using ProyectoAutoPartes.Models; // Asegúrate de que este `using` esté presente

namespace ProyectoAutoPartes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Agregar aquí los DbSet para cada modelo
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>().ToTable("Productos");
        }
    }
}

