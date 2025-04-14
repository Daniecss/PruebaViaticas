using Microsoft.EntityFrameworkCore;
using CinemaAPP.Models;

namespace CinemaAPP.Data
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
        { }

        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<SalaCine> SalasCine { get; set; }
        public DbSet<PeliculaSalaCine> PeliculasSalasCine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PeliculaSalaCine>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<PeliculaSalaCine>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();  // Esto asegura que el valor de los Id se genere automáticamente

            // Relación muchos a muchos entre Pelicula y SalaCine
            modelBuilder.Entity<PeliculaSalaCine>()
                .HasKey(ps => new { ps.IdPelicula, ps.IdSalaCine });

            modelBuilder.Entity<PeliculaSalaCine>()
                .HasOne(ps => ps.Pelicula)
                .WithMany(p => p.PeliculasSalasCine)
                .HasForeignKey(ps => ps.IdPelicula);

            modelBuilder.Entity<PeliculaSalaCine>()
                .HasOne(ps => ps.SalaCine)
                .WithMany(s => s.PeliculasSalasCine)
                .HasForeignKey(ps => ps.IdSalaCine);
        }
    }
}

