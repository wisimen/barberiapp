using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Horario
{
    public class HorarioCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Dia { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Time, ErrorMessage = "El campo {0} no cumple con el formato")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Time, ErrorMessage = "El campo {0} no cumple con el formato")]
        public TimeSpan HoraFin { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoBarberia { get; set; }
    }
}

