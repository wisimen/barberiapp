using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class TipoServicio
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoTipoServicio { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Nombre { get; set; }

        // Referencias
        public ICollection<Servicio> Servicios { get; set; }
    }
}

