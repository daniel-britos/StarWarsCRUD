namespace StarWarsCRUD.Api.DTOs.Request.Create;
public class CreateDatosBiograficosRequest
{
    public int PersonajeId { get; init; }
    public double AlturaCm { get; init; }
    public string? ColorOjos { get; init; }
    public string? Especie { get; init; }
}