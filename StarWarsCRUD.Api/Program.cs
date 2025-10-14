var builder = WebApplication.CreateBuilder(args);


// AQUÍ CONECTAMOS LAS CAPAS DEL PROYECTO
// Le decimos: "Cuando alguien pida un IPersonajeRepository, entrégale una instancia de PersonajeRepository"
//builder.Services.AddScoped<IPersonajeRepository, PersonajeRepository>();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
