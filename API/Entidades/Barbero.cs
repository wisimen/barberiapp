using Barberiapp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class Barbero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoBarbero { get; set; }

        public int CodigoBarberia { get; set; }

        public string CodigoUsuario { get; set; }

        // Referencias

        [ForeignKey("CodigoUsuario")]
        public ApplicationUser Usuario { get; set; }

        [ForeignKey("CodigoBarberia")]
        public Barberia Barberia { get; set; }

        public ICollection<Cita> Citas { get; set; }

        public ICollection<FotoCorte> FotosCortes { get; set; }
    }
}

