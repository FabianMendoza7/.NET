using Payments.Entidades;
using System;
using System.Threading.Tasks;

namespace Payments.Repositorio.Facturacion
{
    public class FacturacionRepository
    {
        private readonly ApplicationDbContext _context;

        public FacturacionRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<Factura> FacturarPedido(Guid numeroFactura, Pedido pedido)
        {
            Factura factura = new Factura()
            {
                Numero = numeroFactura,
                PedidoId = pedido.Id,
                Pedido = pedido
            };

            _context.Add(factura);
            await _context.SaveChangesAsync();

            return factura;
        }
    }
}
