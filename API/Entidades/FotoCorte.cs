using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class FotoCorte
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoFotoCorte { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.ImageUrl, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string URL_Foto { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoBarberia { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CodigoBarbero { get; set; }

        // Referencias
        [ForeignKey("CodigoBarberia")]
        public Barberia Barberia { get; set; }

        [ForeignKey("CodigoBarbero")]
        public Barbero Barbero { get; set; }
    }
}

