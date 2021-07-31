using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments.Entidades
{
    public class Factura
    {
        // El tipo GUID es a manera de ejemplo
        public Guid Numero { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

    }
}
