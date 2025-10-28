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

    /*para un proyecto de Domain Driven Design (DDD), donde 
      la lógica de negocio y las restricciones son clave, es imprescindible
      usar el Fluent API (OnModelCreating o IEntityTypeConfiguration) 
      para asegurar que el modelo de la base de datos cumpla exactamente
     con las reglas de tu dominio.*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*
         función principal es decirle a EF Core cómo debe mapear tus 
         clases de C# (tus entidades de dominio) a las estructuras de la 
         base de datos (tablas, columnas, relaciones, etc.) 
         
        Este método es donde se utiliza el Fluent API de EF Core, que 
        permite una configuración detallada que va más allá de lo que las
        convenciones o los DataAnnotations pueden ofrecer.
         */
        base.OnModelCreating(modelBuilder);

        // Se usa typeof(ApplicationDbContext).Assembly para obtener el ensamblado
        // del proyecto StarWarsCRUD.Infrastructure, que contiene tanto el DbContext
        // como todas las clases de configuración (NaveConfiguration, PeliculaConfiguration, etc.)

        // Carga modular de todas las configuraciones
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

}
