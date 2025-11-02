namespace StarWarsCRUD.Api.Middlewares;
public class BloqueaPeticionMiddleware
{
    private readonly RequestDelegate _next;

    public BloqueaPeticionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext contexto)
    {
        if (contexto.Request.Path == "/bloqueado")
        {
            contexto.Response.StatusCode = 403; // Forbidden
            await contexto.Response.WriteAsync("acceso denegado");
        }
        else
        {
            await _next.Invoke(contexto);
        }
    }
}

public static class BLoqueaPeticionMiddlewareExtensions
{
    public static IApplicationBuilder UseBloqueaPeticion(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BloqueaPeticionMiddleware>();
    }
}