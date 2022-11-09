using Barberiapp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barberiapp.Entidades
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoCliente { get; set; }
        public string CodigoUsuario { get; set; }

        // Referencias
        [ForeignKey("CodigoUsuario")]
        public ApplicationUser Usuario { get; set; }
        public ICollection<Cita> Citas { get; set; }
    }
}

