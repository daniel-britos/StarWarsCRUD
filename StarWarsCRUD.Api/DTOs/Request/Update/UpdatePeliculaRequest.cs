namespace StarWarsCRUD.Api.DTOs.Request.Update;

public class UpdatePeliculaRequest
{
    public string? Titulo { get; init; }
    public DateOnly? FechaEstreno { get; init; }
    public int[]? PersonajeIds { get; init; } // sincronizar relaciones opcional
}