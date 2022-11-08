using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.TipoServicio
{
    public class TipoServicioCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Nombre { get; set; }
    }
}

