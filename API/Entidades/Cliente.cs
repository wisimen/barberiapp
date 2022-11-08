using Barberiapp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    public class Cliente : IdentityModels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoCliente { get; set; }

        // Referencias
        public ICollection<Cita> Citas { get; set; }
    }
}

