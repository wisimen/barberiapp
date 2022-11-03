using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.ImagenVehiculo
{
    public class ImagenVehiculoActualizacionArchivoDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoVehiculo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public IFormFile Imagen { get; set; }
    }
}
