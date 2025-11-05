using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StarWarsCRUD.Api.DTOs.Request.Create;
using StarWarsCRUD.Api.DTOs.Response;
using StarWarsCRUD.Domain.Entities;
using StarWarsCRUD.Infrastructure.Data;
using System.Reflection.Metadata.Ecma335;

namespace StarWarsCRUD.Api.Controllers;

[ApiController]
[Route("api/personajes")] //https://localhost:7020/api/Personajes
public class PersonajesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<PersonajesController> _logger;
    private readonly IMapper _mapper;
    public PersonajesController(ApplicationDbContext context,
                                ILogger<PersonajesController> logger,          
                                IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    // Get con datos de columnas combinados
    [HttpGet] 
    public async Task<IEnumerable<PersonajeResponse>> Get()
    {
        //Sin Automapper
        //var personajes = await _context.Personajes.ToListAsync();
        //var personajesDto = personajes.Select(p => new PersonajeResponse{ Id = p.Id,
        //                                                Nombre = $"{p.Nombre} {p.Descripcion}"});

        // Con Automapper
        var personajes = await _context.Personajes.ToListAsync();
        //Tomá la lista de personajes que viene de la base de datos y convertímela en una lista del tipo PersonajeResponse usando AutoMapper.
        var personajesDto = _mapper.Map<IEnumerable<PersonajeResponse>>(personajes);

        return personajesDto;
    }

    /*
[HttpGet("{id:int}", Name = "GetPersonajes")]
public async Task<ActionResult<Personaje>> Get(int id)
{
    var personaje = await _context.Personajes.FirstOrDefaultAsync(p => p.Id == id);

    if (personaje == null)
    {
        return NotFound();
    }

    return personaje;
}

[HttpPost("create")]
public async Task<ActionResult> Post(CreatePersonajeRequest createPersonajeRequest)
{
    // Convertimos el DTO de entrada (CreatePersonajeRequest) a la entidad Personaje usando AutoMapper
    var personaje = _mapper.Map<Personaje>(createPersonajeRequest);

    // Indicamos al DbContext que queremos agregar este nuevo personaje a la base de datos
    _context.Add(personaje);

    // Guardamos los cambios en la base de datos (INSERT). 
    // EF Core ejecuta la operación pendiente (Add) y genera el ID del nuevo registro
    await _context.SaveChangesAsync();

    // Convertimos la entidad guardada (con su nuevo Id) a un DTO para devolverlo al cliente
    var personajeDto = _mapper.Map<CreatePersonajeRequest>(personaje);

    // Retornamos HTTP 201 (Created) + la URL donde se puede consultar el nuevo recurso
    return CreatedAtRoute("GetPersonajes", new { id = personaje.Id }, personajeDto);
}

[HttpPut("{id:int}")] // api/personajes/id
public async Task<ActionResult> Put(int id, CreatePersonajeRequest createPersonajeRequest)
{
    var personaje = _mapper.Map<Personaje>(createPersonajeRequest);
    personaje.Id = id;
    _context.Update(personaje);
    await _context.SaveChangesAsync();
    return Ok();
}

[HttpGet("{id:int}", Name = "GetPersonajes")]
public async Task<ActionResult<Personaje>> GetById(int id)
{
    var personaje = await _context.Personajes
                                  .FirstOrDefaultAsync(p => p.Id == id);
    if ( personaje is null )
    {
        return NotFound();
    }

    var personajeDto = _mapper.Map<PersonajeResponse>(personaje);

    return personaje;
}

[HttpDelete("{id:int}")]
public async Task<ActionResult> Delete(int id)
{
    var registrosBorrados = await _context.Personajes
                                          .Where(p => p.Id == id)
                                          .ExecuteDeleteAsync();
    if (registrosBorrados == 0)
    {
        return NotFound();
    }

    return Ok();
}
*/
}
