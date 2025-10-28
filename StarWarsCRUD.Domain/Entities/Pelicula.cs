namespace StarWarsCRUD.Domain.Entities;

public class Pelicula
{
    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public DateOnly FechaEstreno { get; private set; }
    public byte[]? RowVersion { get; private set; }

    private readonly HashSet<Personaje> _personajes = new();
    public IReadOnlyCollection<Personaje> Personajes => _personajes;
    private Pelicula() { }
    public Pelicula(string titulo, DateOnly fechaEstreno)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("El título no puede estar vacío.", nameof(titulo));

        if (fechaEstreno < new DateOnly(1970, 1, 1))
            throw new ArgumentException("Fecha de estreno inválida.", nameof(fechaEstreno));

        Titulo = titulo;
        FechaEstreno = fechaEstreno;
    }

    public void AgregarPersonaje(Personaje personaje)
    {
        if (personaje == null) throw new ArgumentNullException(nameof(personaje));

        if (_personajes.Add(personaje))
        {
            personaje.AgregarAparicionEnPelicula(this);
        }
    }
}