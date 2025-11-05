namespace StarWarsCRUD.Api.DTOs.Response;
public class PersonajeResponse
{
    public int Id { get; init; }
    public string Nombre { get; init; }
    public string? Descripcion { get; init; }    
    public PlanetaSummary? PlanetaNatal { get; init; }
    public IEnumerable<PeliculaSummary>? Peliculas { get; init; }
}