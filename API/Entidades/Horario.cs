using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class Horario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoHorario { get; set; }

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

        // Referencias
        [ForeignKey("CodigoBarberia")]
        public Barberia Barberia { get; set; }
    }
}

