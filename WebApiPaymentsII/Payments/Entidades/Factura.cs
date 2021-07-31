using System;

namespace Payments.Entidades
{
    public class Factura
    {
        // El tipo GUID es a manera de ejemplo.
        public int Id { get; set; }
        public Guid Numero { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}
