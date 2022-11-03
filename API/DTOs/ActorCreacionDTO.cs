using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs
{
    public class ActorCreacionDTO
    {


        [Required]

        [StringLength(50)]
        public string Nombre { get; set; }
        public int Edad { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public IFormFile Foto { get; set; }


    }
}
