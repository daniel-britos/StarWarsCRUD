namespace StarWarsCRUD.Domain.Entities;

public class DatosBiograficos
{
    // PK y FK. EF Core lo configurará de manera automática (PK = FK).
    public int PersonajeId { get; private set; }
    public Personaje Personaje { get; private set; } 
    public double AlturaCm { get; private set; }
    public string? ColorOjos { get; private set; }
    public string? Especie { get; private set; }

    // Concurrency token (opcional)
    public byte[]? RowVersion { get; private set; }

    private DatosBiograficos() { }

    public DatosBiograficos(Personaje personaje, double alturaCm, string? especie)
    {
        if (alturaCm <= 0)
            throw new ArgumentException("La altura debe ser positiva.", nameof(alturaCm));

        Personaje = personaje ?? throw new ArgumentNullException(nameof(personaje));

        AlturaCm = alturaCm;
        Especie = especie;
    }

    // --- Métodos de Actualización ---

    public void ActualizarAltura(double nuevaAltura)
    {
        if (nuevaAltura <= 0)
            throw new ArgumentException("La altura debe ser positiva.", nameof(nuevaAltura));
        AlturaCm = nuevaAltura;
    }

    public void ActualizarEspecie(string nuevaEspecie)
    {
        Especie = nuevaEspecie;
    }
}