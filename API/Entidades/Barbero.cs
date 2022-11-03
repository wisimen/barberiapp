using Barberiapp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class Barbero : IdentityModels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoBarbero { get; set; }


        // Referencias
        public ICollection<Cita> Citas { get; set; }

        public ICollection<TipoServicio> TipoServicio { get; set; }

        public ICollection<FotoCorte> FotosCortes { get; set; }
    }
}

