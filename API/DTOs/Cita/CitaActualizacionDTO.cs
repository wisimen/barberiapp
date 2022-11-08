using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Cita
{
    public class CitaActualizacionDTO : CitaCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoCita { get; set; }
    }
}
