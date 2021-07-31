using Payments.Entidades;
using System.Threading.Tasks;

namespace Payments.Servicios
{
    public interface ILogisticaService
    {
        Task<Envio> EnviarPedido(Factura factura);
        Task ActualizarEstadoEnvio(Envio envio);
    }
}
