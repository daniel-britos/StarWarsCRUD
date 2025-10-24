using StarWarsCRUD.Domain.Enums;
namespace StarWarsCRUD.Api.DTOs.Request.Create;

public class CreatePlanetaRequest
{
    public string Nombre { get; init; } = default!;
    public TipoClima? Clima { get; init; }
}