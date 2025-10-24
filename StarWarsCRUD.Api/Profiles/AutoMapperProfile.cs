using StarWarsCRUD.Api.DTOs.Request.Create;
using StarWarsCRUD.Api.DTOs.Response;
using StarWarsCRUD.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace StarWarsCRUD.Api.Profiles;

public class AutoMapperProfile : Profiles
{
    public AutoMapperProfile()
    {
        // Domain -> Response
        CreateMap<Planeta, PlanetaSummary>();
        CreateMap<Planeta, PlanetaResponse>()
            .ForMember(dest => dest.PersonajesNativos, opt => opt.MapFrom(src => src.PersonajesNativos));

        CreateMap<Pelicula, PeliculaSummary>();
        CreateMap<Pelicula, PeliculaResponse>()
            .ForMember(dest => dest.Personajes, opt => opt.MapFrom(src => src.Personajes));

        CreateMap<Personaje, PersonajeSummary>();
        CreateMap<Personaje, PersonajeResponse>()
            .ForMember(dest => dest.PlanetaNatal, opt => opt.MapFrom(src => src.PlanetaNatal))
            .ForMember(dest => dest.Peliculas, opt => opt.MapFrom(src => src.Peliculas));

        CreateMap<DatosBiograficos, DatosBiograficosResponse>();

        // Request -> Domain (ejemplos prácticos)
        // Para Pelicula y Planeta podemos construir directamente usando los constructores disponibles.
        CreateMap<CreatePeliculaRequest, Pelicula>()
            .ConstructUsing(r => new Pelicula(r.Titulo, r.FechaEstreno));

        CreateMap<CreatePlanetaRequest, Planeta>()
            .ConstructUsing(r => new Planeta(r.Nombre, r.Clima));

        // NOTA: mapping de CreatePersonajeRequest -> Personaje requiere resolver Planeta desde DB
        // y relaciones N:N normalmente se sincronizan en el service/handler; por eso no se incluye aquí.
    }
}