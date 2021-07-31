using Payments.Entidades;
using System.Threading.Tasks;

namespace Payments.Servicios.Facturacion
{
    public interface IFacturacionService
    {
        Task<Factura> FacturarPedido(Pedido pedido);
    }
}
