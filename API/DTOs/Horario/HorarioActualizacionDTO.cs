using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Horario
{
    public class HorarioActualizacionDTO : HorarioCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoHorario { get; set; }
    }
}

