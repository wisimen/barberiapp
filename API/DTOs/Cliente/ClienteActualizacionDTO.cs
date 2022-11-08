using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Cliente
{
    public class ClienteActualizacionDTO : ClienteCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoCliente { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string NuevoPassword { get; set; }

    }
}

