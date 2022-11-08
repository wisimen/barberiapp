using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.FotoCorte
{
    public class FotoCorteActualizacionDTO : FotoCorteCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoFotoCorte { get; set; }
    }
}

