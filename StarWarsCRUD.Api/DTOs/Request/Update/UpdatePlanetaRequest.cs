using StarWarsCRUD.Domain.Enums;

namespace StarWarsCRUD.Api.DTOs.Request.Update;
public class UpdatePlanetaRequest
{
    public string? Nombre { get; init; }
    public TipoClima? Clima { get; init; }
}
