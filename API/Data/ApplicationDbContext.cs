

using Barberiapp.Entidades;
using Barberiapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>()
           .HasOne(p => p.Barbero)
           .WithMany(b => b.Citas)
           .HasForeignKey(p => p.CodigoBarbero)
           .HasPrincipalKey(b => b.CodigoBarbero);

            modelBuilder.Entity<Cita>()
           .HasOne(p => p.Cliente)
           .WithMany(b => b.Citas)
           .HasForeignKey(p => p.CodigoCliente)
           .HasPrincipalKey(b => b.CodigoCliente);

            modelBuilder.Entity<FotoCorte>()
           .HasOne(p => p.Barbero)
           .WithMany(b => b.FotosCortes)
           .HasForeignKey(p => p.CodigoBarbero)
           .HasPrincipalKey(b => b.CodigoBarbero);

            modelBuilder.Entity<Servicio>()
           .HasMany(p => p.TipoServicios)
           .WithMany(b => b.Servicios)
           .UsingEntity(j => j.ToTable("TipoServiciosIncluidos"));

            modelBuilder.Entity<ServiciosCita>().HasKey(d => new
            {
                d.CodigoCita,
                d.CodigoServicio
            });

            modelBuilder.Entity<ServiciosCita>()
           .HasOne(p => p.Cita)
           .WithMany(b => b.ServiciosCita)
           .HasForeignKey(p => p.CodigoCita)
           .HasPrincipalKey(b => b.CodigoCita)
           .OnDelete(DeleteBehavior.NoAction)
           .IsRequired(true);

            modelBuilder.Entity<ServiciosCita>()
           .HasOne(p => p.Servicio)
           .WithMany(b => b.ServiciosCita)
           .HasForeignKey(p => p.CodigoServicio)
           .HasPrincipalKey(b => b.CodigoServicio)
           .OnDelete(DeleteBehavior.NoAction)
           .IsRequired(true);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<ApplicationUser> Usuario { get; set; }

        // Tablas de la base de datos   
        public DbSet<Barberia> Barberia { get; set; }
        public DbSet<Barbero> Barbero { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Cita> Cita { get; set; }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<TipoServicio> TipoServicio { get; set; }
        public DbSet<Horario> Horario { get; set; }
        public DbSet<FotoCorte> FotoCorte { get; set; }
        public DbSet<ServiciosCita> ServiciosCita { get; set; }

    }
}


