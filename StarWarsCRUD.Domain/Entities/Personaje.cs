using System;

namespace StarWarsCRUD.Domain.Entities;

public class Personaje
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string? Descripcion { get; private set; }
    public string? Historia { get; private set; }

    // Concurrency token (opcional, requiere configuración en DbContext)
    public byte[]? RowVersion { get; private set; }

    // --- Relación 1:1 ---
    public DatosBiograficos? DatosBio { get; private set; }

    // --- Relación 1:N (Lado "Muchos") ---
    // FK explícita para mayor claridad / mapping
    public int PlanetaNatalId { get; private set; }
    public Planeta PlanetaNatal { get; private set; }

    // --- Relación N:N ---
    private readonly HashSet<Pelicula> _peliculas = new();
    public IReadOnlyCollection<Pelicula> Peliculas => _peliculas;

    private Personaje() { } // Requerido por EF Core

    public Personaje(string nombre, Planeta planetaNatal)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

        Nombre = nombre;
        PlanetaNatal = planetaNatal ?? throw new ArgumentNullException(nameof(planetaNatal));

        // Nota: PlanetaNatalId lo manejará EF Core; si planetaNatal ya tiene Id, EF sincronizará la FK.
        // Sincronización 1:N (lado planeta)
        planetaNatal.AgregarPersonajeNativo(this);
    }

    // --- Métodos de Dominio (Comportamiento) ---

    public void ActualizarDescripcion(string nuevaDescripcion)
    {
        Descripcion = nuevaDescripcion;
    }

    public void ActualizarHistoria(string nuevaHistoria)
    {
        Historia = nuevaHistoria;
    }

    // ¡Sincronización N:N (Lado A)!
    public void AgregarAparicionEnPelicula(Pelicula pelicula)
    {
        if (pelicula == null) throw new ArgumentNullException(nameof(pelicula));

        // Add devuelve true si se añadió (evita duplicados y llamadas redundantes)
        if (_peliculas.Add(pelicula))
        {
            pelicula.AgregarPersonaje(this);
        }
    }

    public void AsignarDatosBiograficos(DatosBiograficos datos)
    {
        if (datos == null) throw new ArgumentNullException(nameof(datos));

        // Validación 1:1: los datos biográficos deben pertenecer a este personaje.
        if (datos.Personaje != this)
            throw new InvalidOperationException("Los datos biográficos no corresponden a este personaje.");

        DatosBio = datos;
    }
}