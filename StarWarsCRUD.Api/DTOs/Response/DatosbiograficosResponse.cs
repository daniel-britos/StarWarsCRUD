namespace StarWarsCRUD.Api.DTOs.Response;
public class DatosBiograficosResponse
{
    public int PersonajeId { get; init; }
    public double AlturaCm { get; init; }
    public string? ColorOjos { get; init; }
    public string? Especie { get; init; }
}