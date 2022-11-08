

using Barberiapp.Entidades;
using Barberiapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>()
           .HasOne(p => p.Barbero)
           .WithMany(b => b.Citas)
           .HasForeignKey(p => p.CodigoBarbero);

            modelBuilder.Entity<Cita>()
           .HasOne(p => p.Cliente)
           .WithMany(b => b.Citas)
           .HasForeignKey(p => p.CodigoCliente);

            base.OnModelCreating(modelBuilder);

        }


        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<IdentityModels> Usuario { get; set; }
        // Tablas de la base de datos   
        public DbSet<Barberia> Barberia { get; set; }
        public DbSet<Barbero> Barbero { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Cita> Cita { get; set; }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<TipoServicio> TipoServicio { get; set; }
        public DbSet<MediosPago> MediosPago { get; set; }
        public DbSet<Horario> Horario { get; set; }
        public DbSet<FotoCorte> FotoCorte { get; set; }

    }
}


