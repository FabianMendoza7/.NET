using Microsoft.EntityFrameworkCore;
using Payments.Entidades;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Payments.Repositorio.Facturacion
{
    public class FacturacionRepository: IFacturacionRepository
    {
        private readonly ApplicationDbContext _context;

        public FacturacionRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<Factura> CrearFactura(Guid numeroFactura, Pedido pedido)
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

        public async Task<Factura> ObtenerFacturaPorPedidoId(int pedidoId)
        {
            return await _context.Facturas.Include(x => x.Pedido).Where(x => x.PedidoId == pedidoId).FirstOrDefaultAsync();
        }
    }
}
