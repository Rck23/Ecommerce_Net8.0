
using System.Net;
using System.Text.Json;

namespace API.Ayudas.Errores;

public class ExcepcionMiddleware
{
     private readonly RequestDelegate _next;
    private readonly ILogger<ExcepcionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExcepcionMiddleware(RequestDelegate request, ILogger<ExcepcionMiddleware> logger, IHostEnvironment env)
    {
        _next = request;
        _logger = logger;
        _env = env;
    }

    public async Task InvocarAsync(HttpContext context){
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var estatusCodigo = (int)HttpStatusCode.InternalServerError;
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = estatusCodigo; 

            var respuesta  = _env.IsDevelopment() 
                ? new ApiExcepcion(estatusCodigo, ex.Message, ex.StackTrace.ToString())
                : new ApiExcepcion(estatusCodigo);

            var opciones = new JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            };

            var json = JsonSerializer.Serialize(respuesta, opciones); 

            await context.Response.WriteAsync(json);
        }
    }
}
