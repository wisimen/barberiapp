using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class Barberia
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoBarberia { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El campo {0} no es válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Url, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string URL_Ubicacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.ImageUrl, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string Logo { get; set; }

        [DataType(DataType.Url, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string URL_Instagram { get; set; }

        [DataType(DataType.Url, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string URL_Facebook { get; set; }

        [DataType(DataType.Url, ErrorMessage = "El campo {0} no cumple con el formato")]
        public string URL_Youtube { get; set; }

        // Referencias
        public ICollection<Cita> Citas { get; set; }
        public ICollection<Barbero> Barberos { get; set; }
        public ICollection<Horario> Horarios { get; set; }
        public ICollection<Servicio> Servicios { get; set; }
        public ICollection<MediosPago> MediosPago { get; set; }
        public ICollection<FotoCorte> Fotos { get; set; }
    }
}

