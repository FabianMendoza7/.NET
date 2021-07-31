using Payments.Entidades;
using Payments.Repositorio.Logistica;
using System.Threading.Tasks;

namespace Payments.Servicios
{
    public class LogisticaService: ILogisticaService
    {
        private readonly ILogisticaRepository _logisticaRepository;

        public LogisticaService(ILogisticaRepository logisticaRepository)
        {
            this._logisticaRepository = logisticaRepository;
        }

        public async Task<Envio> EnviarPedido(Factura factura)
        {
            return await _logisticaRepository.CrearEnvio(factura);
        }

        public async Task ActualizarEstadoEnvio(Envio envio)
        {
            await _logisticaRepository.ActualizarEstadoEnvio(envio);
        }
    }
}
