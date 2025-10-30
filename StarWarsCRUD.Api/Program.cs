using Microsoft.EntityFrameworkCore;
using StarWarsCRUD.Domain.Interfaces;
using StarWarsCRUD.Infrastructure.Data;
using StarWarsCRUD.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// AQUÍ CONECTAMOS LAS CAPAS DEL PROYECTO
// Le decimos: "Cuando alguien pida un IPersonajeRepository, entrégale una instancia de PersonajeRepository"
//builder.Services.AddScoped<IPersonajeRepository, PersonajeRepository>();
builder.Services.AddScoped<IPersonajeRepository, PersonajeRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
