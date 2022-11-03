using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Barberiapp.DTOs.Autenticacion
{
    public class CredencialesUsuario : IdentityUser
    {
        [Required]
        [EmailAddress]
        public override string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
