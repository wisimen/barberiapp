using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Historial
{
    public class HistorialActualizacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoVehiculo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string ResumenDeMantenimiento { get; set; }
    }
}
