using Payments.Entidades;
using System;
using System.Collections.Generic;

namespace WebApiPayments.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public Cliente Cliente { get; set; }
        public List<PedidoDetalle> Detalle { get; set; }
    }
}
