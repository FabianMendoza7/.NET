using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Payments.DTOs
{
    public class PedidoDetalleCreacionDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal PrecioUnitario { get; set; }
    }
}
