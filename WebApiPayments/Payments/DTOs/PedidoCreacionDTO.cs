using Payments.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments.DTOs
{
    public class PedidoCreacionDTO
    {
        public DateTime Fecha { get; set; }
        public List<PedidoDetalleCreacionDTO> Detalle { get; set; }
    }
}
