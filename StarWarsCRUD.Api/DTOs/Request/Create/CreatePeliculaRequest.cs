namespace StarWarsCRUD.Api.DTOs.Request.Create;
public class CreatePeliculaRequest
{
    public string Titulo { get; init; } = default!;
    public DateOnly FechaEstreno { get; init; }
    public int[]? PersonajeIds { get; init; } // enviar ids para relaciones N:N
}