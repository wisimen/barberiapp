using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Cliente
{
    public class ClienteCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoTipoDocumento { get; set; }

        public IFormFile FotoFile { get; set; }
    }
}

