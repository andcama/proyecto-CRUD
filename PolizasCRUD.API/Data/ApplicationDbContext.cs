using Microsoft.EntityFrameworkCore;
using PolizasCRUD.API.Models;

namespace PolizasCRUD.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Poliza> Polizas { get; set; }
        public DbSet<TipoPoliza> TiposPoliza { get; set; }
        public DbSet<EstadoPoliza> EstadosPoliza { get; set; }
        public DbSet<Cobertura> Coberturas { get; set; }
        public DbSet<PolizaCobertura> PolizaCoberturas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones de relaciones y restricciones
            modelBuilder.Entity<Poliza>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Polizas)
                .HasForeignKey(p => p.CedulaAsegurado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PolizaCobertura>()
                .HasOne(pc => pc.Poliza)
                .WithMany(p => p.PolizaCoberturas)
                .HasForeignKey(pc => pc.NumeroPoliza)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PolizaCobertura>()
                .HasOne(pc => pc.Cobertura)
                .WithMany(c => c.PolizaCoberturas)
                .HasForeignKey(pc => pc.CoberturaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Datos iniciales
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Tipos de Póliza
            modelBuilder.Entity<TipoPoliza>().HasData(
                new TipoPoliza { Id = 1, Nombre = "Vida" },
                new TipoPoliza { Id = 2, Nombre = "Auto" },
                new TipoPoliza { Id = 3, Nombre = "Hogar" },
                new TipoPoliza { Id = 4, Nombre = "Salud" },
                new TipoPoliza { Id = 5, Nombre = "Viaje" }
            );

            // Estados de Póliza
            modelBuilder.Entity<EstadoPoliza>().HasData(
                new EstadoPoliza { Id = 1, Nombre = "Activa" },
                new EstadoPoliza { Id = 2, Nombre = "Cancelada" },
                new EstadoPoliza { Id = 3, Nombre = "Vencida" },
                new EstadoPoliza { Id = 4, Nombre = "En Trámite" }
            );

            // Coberturas
            modelBuilder.Entity<Cobertura>().HasData(
                new Cobertura { Id = 1, Nombre = "Responsabilidad Civil", Descripcion = "Cubre daños a terceros" },
                new Cobertura { Id = 2, Nombre = "Robo", Descripcion = "Cubre pérdidas por robo" },
                new Cobertura { Id = 3, Nombre = "Incendio", Descripcion = "Cubre daños por incendio" },
                new Cobertura { Id = 4, Nombre = "Daños materiales", Descripcion = "Cubre daños a la propiedad asegurada" },
                new Cobertura { Id = 5, Nombre = "Accidentes personales", Descripcion = "Cubre lesiones personales" },
                new Cobertura { Id = 6, Nombre = "Asistencia médica", Descripcion = "Cubre gastos médicos" },
                new Cobertura { Id = 7, Nombre = "Fallecimiento", Descripcion = "Indemnización por fallecimiento" }
            );
        }
    }
}