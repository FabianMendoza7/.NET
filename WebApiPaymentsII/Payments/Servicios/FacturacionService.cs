using Payments.Entidades;
using Payments.Repositorio.Facturacion;
using System;
using System.Threading.Tasks;

namespace Payments.Servicios.Facturacion
{
    public class FacturacionService: IFacturacionService
    {
        private readonly IFacturacionRepository _facturacionRepository;

        public FacturacionService(IFacturacionRepository facturacionRepository)
        {
            this._facturacionRepository = facturacionRepository;
        }

        public async Task<Factura> FacturarPedido(Pedido pedido)
        {
            // El servicio genera el número de la factura o la obtiene de un sistema externo de facturación electrónica.
            // No se realizan ni se guardan cálculos, esto es propio de un front-end.
            Guid numeroFactura = Guid.NewGuid();

            // Se realiza persistencia de la factura.
            return await _facturacionRepository.FacturarPedido(pedido);
        }
    }
}
