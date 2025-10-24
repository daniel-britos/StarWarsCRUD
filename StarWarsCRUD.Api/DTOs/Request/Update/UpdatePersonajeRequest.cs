using System.ComponentModel.DataAnnotations;

namespace StarWarsCRUD.Api.DTOs.Request.Update;
public class UpdatePersonajeRequest
{
    [StringLength(1000)] 
    public string? Descripcion { get; init; }
    public string? Historia { get; init; }
    public List<int>? PeliculaIds { get; init; } // opcional para sincronizar relaciones
}



