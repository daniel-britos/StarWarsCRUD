using StarWarsCRUD.Domain.Enums;

namespace StarWarsCRUD.Api.DTOs.Response;
public class PlanetaResponse
{
    public int Id { get; init; }
    public string Nombre { get; init; }
    public TipoClima? Clima { get; init; }
    public IEnumerable<PersonajeSummary>? PersonajesNativos { get; init; }
}
