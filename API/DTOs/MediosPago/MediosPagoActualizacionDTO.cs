using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.MediosPago
{
    public class MediosPagoActualizacionDTO : MediosPagoCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoMedioPago { get; set; }
    }
}
