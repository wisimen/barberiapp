using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.TipoServicio
{
    public class TipoServicioActualizacionDTO : TipoServicioCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoTipoServicio { get; set; }
    }
}

