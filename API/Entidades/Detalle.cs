using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class Detalle
    {

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoHistorial { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoProcedimiento { get; set; }

        // Referencias

        [ForeignKey("CodigoHistorial")]
        public Historial Historial { get; set; }

        [ForeignKey("CodigoProcedimiento")]
        public Procedimiento Procedimiento { get; set; }
    }
}