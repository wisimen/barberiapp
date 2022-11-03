using Barberiapp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class Vehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Linea { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Color { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string CodigoUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoTipoVehiculo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoMarca { get; set; }

        // Referencias

        [ForeignKey("CodigoUsuario")]
        public IdentityModels Usuario { get; set; }

        [ForeignKey("CodigoTipoVehiculo")]
        public TipoVehiculo TipoVehiculo { get; set; }

        [ForeignKey("CodigoMarca")]
        public Marca Marca { get; set; }

        public ICollection<ImagenVehiculo> Imagenes { get; set; }
        public ICollection<Historial> Historiales { get; set; }
    }
}
