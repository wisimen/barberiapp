using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Detalle
{
    public class DetalleDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoHistorial { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoProcedimiento { get; set; }
    }
}