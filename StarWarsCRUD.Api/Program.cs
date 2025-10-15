using Microsoft.EntityFrameworkCore;
using StarWarsCRUD.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// AQU� CONECTAMOS LAS CAPAS DEL PROYECTO
// Le decimos: "Cuando alguien pida un IPersonajeRepository, entr�gale una instancia de PersonajeRepository"
//builder.Services.AddScoped<IPersonajeRepository, PersonajeRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
