using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsCRUD.Api.DTOs.Request.Create;
using StarWarsCRUD.Api.DTOs.Response;
using StarWarsCRUD.Domain.Entities;

namespace StarWarsCRUD.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonajesController : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreatePersonaje([FromBody] CreatePersonajeRequest request)
    {
        // ✅ Validación automática vía DataAnnotations (ModelState)
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // ✅ Verificar existencia de Planeta
        var planeta = await _context.Planetas
            .FirstOrDefaultAsync(p => p.Id == request.PlanetaNatalId);

        if (planeta == null)
            return NotFound($"El planeta con ID {request.PlanetaNatalId} no existe.");

        // ✅ Crear instancia de Personaje (regla de negocio en constructor)
        var personaje = new Personaje(request.Nombre, planeta);

        // ✅ Asignar opcionales
        if (!string.IsNullOrWhiteSpace(request.Descripcion))
            personaje.ActualizarDescripcion(request.Descripcion);

        if (!string.IsNullOrWhiteSpace(request.Historia))
            personaje.ActualizarHistoria(request.Historia);

        // ✅ Agregar relaciones N:N (Películas)
        if (request.PeliculaIds?.Any() == true)
        {
            var peliculas = await _context.Peliculas
                .Where(p => request.PeliculaIds.Contains(p.Id))
                .ToListAsync();

            foreach (var pelicula in peliculas)
                personaje.AgregarAparicionEnPelicula(pelicula);
        }

        // ✅ Persistir cambios
        _context.Personajes.Add(personaje);
        await _context.SaveChangesAsync();

        // ✅ Preparar respuesta
        var response = _mapper.Map<PersonajeResponse>(personaje);

        // ✅ Devolver respuesta 201 Created con ruta del recurso
        return CreatedAtAction(nameof(GetById), new { id = personaje.Id }, response);
    }

    // ✅ Endpoint auxiliar: obtener personaje por id (para CreatedAtAction)
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PersonajeResponse>> GetById(int id)
    {
        var personaje = await _context.Personajes
            .Include(p => p.PlanetaNatal)
            .Include(p => p.Peliculas)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (personaje == null)
            return NotFound();

        return _mapper.Map<PersonajeResponse>(personaje);
    }
}
