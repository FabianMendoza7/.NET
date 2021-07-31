using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Payments.Filtros
{
    public class Excepciones : ExceptionFilterAttribute
    {
        private readonly ILogger<Excepciones> _logger;

        public Excepciones(ILogger<Excepciones> logger)
        {
            this._logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }
    }
}
