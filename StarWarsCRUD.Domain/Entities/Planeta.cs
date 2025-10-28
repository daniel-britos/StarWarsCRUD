using StarWarsCRUD.Domain.Enums;

namespace StarWarsCRUD.Domain.Entities;

public class Planeta
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public TipoClima? Clima { get; private set; }
    public byte[]? RowVersion { get; private set; }

    // --- Relación 1:N (Lado "Uno") ---
    private readonly HashSet<Personaje> _personajesNativos = new();
    public IReadOnlyCollection<Personaje> PersonajesNativos => _personajesNativos;

    private Planeta() { }

    public Planeta(string nombre, TipoClima? clima = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del planeta no puede estar vacío.", nameof(nombre));

        Nombre = nombre;
        Clima = clima;
    }

    public void AgregarPersonajeNativo(Personaje personaje)
    {
        if (personaje == null) throw new ArgumentNullException(nameof(personaje));
        _personajesNativos.Add(personaje);
    }

    public void ActualizarClima(TipoClima nuevoClima)
    {
        Clima = nuevoClima;
    }
}