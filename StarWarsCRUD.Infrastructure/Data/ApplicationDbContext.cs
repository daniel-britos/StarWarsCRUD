using Microsoft.EntityFrameworkCore;
using StarWarsCRUD.Domain.Entities;

namespace StarWarsCRUD.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Personaje> Personajes => Set<Personaje>();
    public DbSet<Pelicula> Peliculas => Set<Pelicula>();
    public DbSet<Planeta> Planetas => Set<Planeta>();
    public DbSet<Nave> Naves => Set<Nave>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // configuración de RowVersion para el manejo de concurrencia al actualizar entidades
        modelBuilder.Entity<Personaje>()
            .Property(p => p.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        modelBuilder.Entity<Pelicula>()
            .Property(p => p.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        modelBuilder.Entity<Planeta>()
            .Property(p => p.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        modelBuilder.Entity<Nave>()
            .Property(d => d.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}
