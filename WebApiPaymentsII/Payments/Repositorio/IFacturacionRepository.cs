using Payments.Entidades;
using System;
using System.Threading.Tasks;

namespace Payments.Repositorio.Facturacion
{
    public interface IFacturacionRepository
    {
        Task<Factura> CrearFactura(Guid numeroFactura, Pedido pedido);

        Task<Factura> ObtenerFacturaPorPedidoId(int pedidoId);
    }
}
