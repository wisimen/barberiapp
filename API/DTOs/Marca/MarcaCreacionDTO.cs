using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Marca
{
    public class MarcaCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Nombre { get; set; }
    }
}
