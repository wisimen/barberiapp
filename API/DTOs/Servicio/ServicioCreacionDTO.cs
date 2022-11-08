using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Servicio
{
    public class ServicioCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Currency, ErrorMessage = "El campo {0} no cumple con el formato")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Text, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoBarberia { get; set; }
    }
}
