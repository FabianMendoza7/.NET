using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Payments.Middlewares
{
    public static class LogearRespuestaHTTPExtensions
    {
        public static IApplicationBuilder UseLogearRespuestaHTTP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogearRespuestaHTTP>();
        }
    }

    public class LogearRespuestaHTTP
    {
        private readonly RequestDelegate _siguiente;
        private readonly ILogger<LogearRespuestaHTTP> _logger;

        public LogearRespuestaHTTP(RequestDelegate siguiente, ILogger<LogearRespuestaHTTP> logger)
        {
            this._siguiente = siguiente;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            using (var ms = new MemoryStream())
            {
                var orginalBodyRes = contexto.Response.Body;
                contexto.Response.Body = ms;

                await _siguiente(contexto);

                ms.Seek(0, SeekOrigin.Begin);
                string respuesta = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(orginalBodyRes);
                contexto.Response.Body = orginalBodyRes;

                _logger.LogInformation(respuesta);
            }
        }
    }
}
