using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Payments.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} no debe tener menos de {1} caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} no debe tener menos de {1} caracteres")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} no debe tener menos de {1} caracteres")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 10, ErrorMessage = "El campo {0} no debe tener menos de {1} caracteres")]
        public string Estado { get; set; }
        public Cliente Cliente { get; set; }
        public List<PedidoDetalle> Detalle { get; set; }
    }
}
