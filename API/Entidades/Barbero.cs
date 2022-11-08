using Barberiapp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class Barbero : IdentityModels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoBarbero { get; set; }

        public int CodigoBarberia { get; set; }


        // Referencias
        [ForeignKey("CodigoBarberia")]
        public Barberia Barberia { get; set; }

        public ICollection<Cita> Citas { get; set; }

        public ICollection<TipoServicio> TipoServicio { get; set; }

        public ICollection<FotoCorte> FotosCortes { get; set; }
    }
}

