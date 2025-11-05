using System.ComponentModel.DataAnnotations;

namespace StarWarsCRUD.Api.DTOs.Request.Create;

public class CreatePersonajeRequest
{    
    [Required] public string Nombre { get; init; }
    [Required] public int PlanetaNatalId { get; init; }
    [StringLength(1000)] public string? Descripcion { get; init; }
    public List<int>? PeliculaIds { get; init; }
}

