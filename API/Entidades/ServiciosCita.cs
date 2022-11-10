using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class ServiciosCita
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoCita { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoServicio { get; set; }

        // Referencias
        [ForeignKey("CodigoServicio")]
        public Servicio Servicio { get; set; }

        [ForeignKey("CodigoCita")]
        public Cita Cita { get; set; }
    }
}

