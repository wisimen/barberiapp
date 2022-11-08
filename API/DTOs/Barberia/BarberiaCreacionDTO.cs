using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Barberia
{
    public class BarberiaCreacionDTO
    {

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El campo {0} no es válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Url, ErrorMessage = "El campo {0} no cumple con el formato")]
        [MaxLength(300, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string URL_Ubicacion { get; set; }

        [DataType(DataType.Url, ErrorMessage = "El campo {0} no cumple con el formato")]
        [MaxLength(300, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string URL_Instagram { get; set; }

        [DataType(DataType.Url, ErrorMessage = "El campo {0} no cumple con el formato")]
        [MaxLength(300, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string URL_Facebook { get; set; }

        [DataType(DataType.Url, ErrorMessage = "El campo {0} no cumple con el formato")]
        [MaxLength(300, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string URL_Youtube { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public IFormFile LogoFile { get; set; }
    }
}