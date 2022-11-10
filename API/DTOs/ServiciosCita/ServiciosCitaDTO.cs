using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.ServiciosCita
{
    public class ServiciosCitaDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoCita { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoServicio { get; set; }
    }
}

