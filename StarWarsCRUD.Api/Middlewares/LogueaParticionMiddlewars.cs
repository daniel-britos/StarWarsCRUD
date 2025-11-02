namespace StarWarsCRUD.Api.Middlewares;

public class LogueaParticionMiddlewars
{
    private readonly RequestDelegate _next;

    public LogueaParticionMiddlewars(RequestDelegate next)
    {
        _next = next;
    }
    public async  Task InvokeAsync(HttpContext contexto)
    {
        // viene la petición 
        var logger = contexto.RequestServices
                             .GetRequiredService<ILogger<Program>>();
        logger.LogInformation($"Petición: {contexto.Request.Method} {contexto.Request.Path}");

        // continua el flujo hacia el siguiente middleware 
        await _next.Invoke(contexto);

        //se va la respuesta
        logger.LogInformation($"Respuesta: {contexto.Response.StatusCode}");
    }
}

// metodo de extensión para agregar el middleware al pipeline 
public static class LogueaPeticionMiddlewareExtensions
{
    public static IApplicationBuilder UseLogueaPeticion(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LogueaParticionMiddlewars>();
    }
}


