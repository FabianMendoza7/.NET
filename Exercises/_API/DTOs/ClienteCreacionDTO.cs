using System.ComponentModel.DataAnnotations;

namespace Payments.DTOs
{
    public class ClienteCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} no debe tener menos de {1} caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} no debe tener menos de {1} caracteres")]
        public string Apellidos { get; set; }
    }
}
