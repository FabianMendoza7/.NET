using System;

namespace Payments.Entidades
{
    public class Envio
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public string Estado { get; set; }
        public DateTime FechaEstado { get; set; }
    }
}
