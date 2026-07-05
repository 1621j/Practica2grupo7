using System.Net;

namespace Practica2Grupo7.UI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo una excepción.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    esCorrecto = false,
                    mensaje = "Ha ocurrido un error inesperado."
                });
            }
        }
    }
}