using System.ComponentModel.DataAnnotations;
/* 
 diseño de dominio (DDD
 */
namespace StarWarsCRUD.Domain.Entities;

public class Personaje
{
    public int Id { get; private set; }
    // al colocar las propiedades en private set evitamos
    // que se modifiquen desde fuera de la clase
    // no utilizamos [Required] por que ya está en el constructor la validación, tampoco es necesario en el modelbuilder
    public string Nombre { get; private set; }
    public string? Descripcion { get; private set; }
    // RowVersion es una columna
    // especial que ayuda a manejar situaciones donde 
    // múltiples usuarios intentan actualizar el mismo
    // registro al mismo tiempo.
    public byte[]? RowVersion { get; private set; }        
    // FK explícita para mayor claridad / mapping
    // PlanetaNatalId es la clave foránea con Planeta
    public int PlanetaNatalId { get; private set; }
    // Propiedad de navegación hacia el planeta natal
    public Planeta PlanetaNatal { get; private set; }
    // Relación N:N (muchos a muchos) con Pelicula
    // HashSet para evitar duplicados automáticamente    
    private readonly HashSet<Pelicula> _peliculas = new();
    // Exponer solo lectura para encapsulación
    public IReadOnlyCollection<Pelicula> Peliculas => _peliculas; 
    // Relación 1:N lado uno.
    private readonly HashSet<Nave> _naves = new();
    public IReadOnlyCollection<Nave> Naves => _naves;

    // Creamos un constructor en privado para EF Core y un
    // constructor en publico para la creación de instancias válidas
    private Personaje() { }

    // Valida y asigna Nombre y PlanetaNatal, lanza excepciones si son
    // inválidos y registra el Personaje en el Planeta.
    public Personaje(string nombre, Planeta planetaNatal)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

        Nombre = nombre;
        PlanetaNatal = planetaNatal ?? throw new ArgumentNullException(nameof(planetaNatal));

        // Nota: PlanetaNatalId lo manejará EF Core; si planetaNatal ya tiene Id,
        // EF sincronizará la FK.
        // Sincronización 1:N (lado planeta)
        planetaNatal.AgregarPersonajeNativo(this);
    }

    public void AgregarNave(Nave nave)
    {
        if (nave == null) throw new ArgumentNullException(nameof(nave));
        _naves.Add(nave);
    }

    public void ActualizarDescripcion(string nuevaDescripcion)
    {
        Descripcion = nuevaDescripcion;
    }

    public void AgregarAparicionEnPelicula(Pelicula pelicula)
    {
        if (pelicula == null) throw new ArgumentNullException(nameof(pelicula));

        // Si .Add() devuelve true, significa que la película no estaba
        // y la hemos añadido.
        if (_peliculas.Add(pelicula))
        {
            // Sincronizamos el otro lado (Pelicula)
            pelicula.AgregarPersonaje(this);
        }
    }
}
