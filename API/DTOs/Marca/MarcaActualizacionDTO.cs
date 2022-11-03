using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Marca
{
    public class MarcaActualizacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Nombre { get; set; }
    }
}
