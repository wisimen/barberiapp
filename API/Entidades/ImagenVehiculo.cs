using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class ImagenVehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(5000, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Ruta { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoVehiculo { get; set; }

        // Referencias

        [ForeignKey("CodigoVehiculo")]
        public Vehiculo Vehiculo { get; set; }
    }
}
