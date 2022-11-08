using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Barberia
{
    public class BarberiaActualizacionDTO : BarberiaCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoBarberia { get; set; }
    }
}