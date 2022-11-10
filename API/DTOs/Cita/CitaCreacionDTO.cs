using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Cita
{
    public class CitaCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El campo {0} no cumple con el formato")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Time, ErrorMessage = "El campo {0} no cumple con el formato")]
        public TimeSpan Hora { get; set; }

        public int Valoracion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Comentario { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Estado { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoBarbero { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoCliente { get; set; }
    }
}
