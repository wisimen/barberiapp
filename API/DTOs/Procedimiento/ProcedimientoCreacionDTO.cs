using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Procedimiento
{
    public class ProcedimientoCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(5000, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Descripcion { get; set; }
    }
}
