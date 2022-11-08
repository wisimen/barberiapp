using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.FotoCorte
{
    public class FotoCorteCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public IFormFile FotoFile { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoBarberia { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoBarbero { get; set; }
    }
}

