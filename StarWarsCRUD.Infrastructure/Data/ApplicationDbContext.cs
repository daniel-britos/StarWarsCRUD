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

        // Carga modular de todas las configuraciones
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
