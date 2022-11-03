using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Vehiculo
{
    public class VehiculoCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Linea { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Color { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string CodigoUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoTipoVehiculo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoMarca { get; set; }
    }
}
