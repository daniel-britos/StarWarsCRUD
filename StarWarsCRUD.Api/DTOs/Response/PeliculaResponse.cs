namespace StarWarsCRUD.Api.DTOs.Response;
public class PeliculaResponse
{
    public int Id { get; init; }
    public string Titulo { get; init; }
    public DateOnly FechaEstreno { get; init; }
    public IEnumerable<PersonajeSummary>? Personajes { get; init; }
}
