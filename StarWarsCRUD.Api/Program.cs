using Microsoft.EntityFrameworkCore;
using StarWarsCRUD.Api.Middlewares;
using StarWarsCRUD.Domain.Interfaces;
using StarWarsCRUD.Infrastructure.Data;
using StarWarsCRUD.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Le decimos: "Cuando alguien pida un IPersonajeRepository, entrégale una instancia de PersonajeRepository"
//AddScoped crea una instancia por cada petición, evitando errores y conflictos cuando varios usuarios acceden a la API simultáneamente.
builder.Services.AddScoped<IPersonajeRepository, PersonajeRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// area configuración de servicios

// 1. AÑADIMOS EL MANEJADOR DE EXCEPCIONES AL CONTENEDOR DE SERVICIOS
// Le decimos a .NET: "Tengo un manejador de excepciones personalizado llamado GlobalExceptionHandler"
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// (Importante) También necesitamos este servicio para que ProblemDetails funcione correctamente
builder.Services.AddProblemDetails();

// fin del area de configuración de servicios

var app = builder.Build();

// 2. AÑADIMOS EL MIDDLEWARE AL PIPELINE
// Le decimos a .NET: "Activa el middleware de gestión de excepciones"
// Este debe ir MUY ARRIBA en el pipeline para atrapar errores de middlewares posteriores.
app.UseExceptionHandler();

// agregamos el middleware personalizado para logueo de peticiones
//app.UseLogueaPeticion();

//app.UseBloqueaPeticion();

//app.MapGet("/", () => "Hello World!");

app.Run();
 