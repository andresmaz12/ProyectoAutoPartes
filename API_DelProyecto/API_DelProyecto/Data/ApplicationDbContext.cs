using Microsoft.EntityFrameworkCore;
using API_DelProyecto.Models;

namespace API_DelProyecto.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para Productos
            modelBuilder.Entity<Producto>().ToTable("Productos");
            modelBuilder.Entity<Producto>().Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Producto>().Property(p => p.Precio).HasPrecision(10, 2);
            modelBuilder.Entity<Producto>().Property(p => p.Marca).HasMaxLength(50);
            modelBuilder.Entity<Producto>().Property(p => p.Año).HasMaxLength(20);
            modelBuilder.Entity<Producto>().Property(p => p.Modelo).HasMaxLength(100);
            modelBuilder.Entity<Producto>().Property(p => p.Imagen).HasMaxLength(255);
            
            // Configuración para Cotizaciones
            modelBuilder.Entity<Cotizacion>().ToTable("Cotizaciones");
            modelBuilder.Entity<Cotizacion>().Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Cotizacion>().Property(c => c.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Cotizacion>().Property(c => c.Telefono).HasMaxLength(20);
            modelBuilder.Entity<Cotizacion>().Property(c => c.Mensaje).HasMaxLength(500);
            
            // Relación entre Cotización y Producto
            modelBuilder.Entity<Cotizacion>()
                .HasOne(c => c.Producto)
                .WithMany()
                .HasForeignKey(c => c.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}