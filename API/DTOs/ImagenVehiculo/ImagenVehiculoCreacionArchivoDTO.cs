using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.ImagenVehiculo
{
    public class ImagenVehiculoCreacionArchivoDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public IFormFile Imagen { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoVehiculo { get; set; }
    }
}
