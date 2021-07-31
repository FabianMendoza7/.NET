using Payments.Entidades;
using System.Threading.Tasks;

namespace Payments.Repositorio.Facturacion
{
    public interface IFacturacionRepository
    {
        Task<Factura> FacturarPedido(Pedido pedido);
    }
}
