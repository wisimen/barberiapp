using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Barbero
{
    public class BarberoActualizacionDTO : BarberoCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoBarbero { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string NuevoPassword { get; set; }
    }
}

