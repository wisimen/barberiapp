using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.TipoDocumento
{
    public class TipoDocumentoActualizacionDTO : TipoDocumentoCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoTipoDocumento { get; set; }

    }
}
