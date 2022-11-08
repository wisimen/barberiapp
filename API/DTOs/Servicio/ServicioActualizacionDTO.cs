using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Servicio
{
    public class ServicioActualizacionDTO : ServicioCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoServicio { get; set; }
    }
}
