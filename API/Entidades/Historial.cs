using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class Historial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoVehiculo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string ResumenDeMantenimiento { get; set; }

        // Referencias

        public ICollection<Detalle> Detalles { get; set; }

        [ForeignKey("CodigoVehiculo")]
        public Vehiculo Vehiculo { get; set; }
    }
}
