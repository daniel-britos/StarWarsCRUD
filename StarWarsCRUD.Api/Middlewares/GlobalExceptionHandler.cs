using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StarWarsCRUD.Api.Middlewares;
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    // Usamos ILogger para registrar el error
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
                                                Exception exception, 
                                                CancellationToken cancellationToken)
    {
        // 1. REGISTRAR EL ERROR (LOGGING)
        // Registramos el error con todo el detalle (stack trace) en nuestra consola o sistema de logs.
        // Esto es para los desarrolladores.
        _logger.LogError(exception, "Ocurrió una excepción no controlada: {Mensaje}", exception.Message);

        // 2. RESPUESTA AMIGABLE (PARA EL CLIENTE)
        // Usamos ProblemDetails, que es un estándar (RFC 7807) para reportar errores en APIs.
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Error interno del servidor",
            Detail = $"Ocurrió un error inesperado. Por favor, intente de nuevo más tarde: {exception.ToString()}"
        };

        // (Opcional) Si estás en Desarrollo, se puede añadir más detalles:
        // if (_env.IsDevelopment())
        // {
        //     problemDetails.Detail = exception.ToString();
        // }

        // 3. ENVIAR LA RESPUESTA
        // Establecemos el código de estado y escribimos la respuesta JSON.
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        // 4. INDICAR QUE EL ERROR FUE MANEJADO
        // Devolvemos 'true' para decirle al pipeline de .NET que este error
        // ya fue gestionado y no necesita seguir propagándose.
        return true;
    }
}
