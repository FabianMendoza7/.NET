using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payments.Entidades
{
    public class PedidoDetalle
    {
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal PrecioUnitario { get; set; }
        public Producto Producto { get; set; }
    }
}
