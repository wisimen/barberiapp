using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.Autenticacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Barberiapp.Controllers
{
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        public readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<CuentasController> logger;

        public CuentasController(UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context,
            IMapper mapper,
            ILogger<CuentasController> logger)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }


        protected async Task<RespuestaAutenticacion> ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("Email", credencialesUsuario.Email),
                new Claim("HoraLogin", DateTime.Now.Ticks.ToString()),
                new Claim("BasicUser", true.ToString())
            };

            var usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(2);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };
        }

        [HttpPost("login")] // Inicio de sesión
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
        {

            var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.Email,
                credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

        [HttpPost("AsignarRol")]
        [AllowAnonymous]
        public async Task<ActionResult> AddClaimsToUser(string email, string claimname, string claimValue, string permissionKey)
        {
            var key = configuration["permissionKey"];
            if (permissionKey != key)
            {
                return BadRequest("No tienes permisos para realizar esta acción");
            }
            //Validar si el usario existe
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                logger.LogInformation($"El usuario elegido con el email: {email}, no existe");
                return BadRequest(new
                {
                    error = "El usario no existe"
                });

            }

            var userClaim = new Claim(claimname, claimValue);
            var result = await userManager.AddClaimAsync(user, userClaim);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = $"Al usuario {user.Email}, se le asignó el claim de: {claimname} "
                });
            }
            return BadRequest(new
            {
                error = $"No fue posible asignarle el Claim: {claimname} al usuario {user.Email}"
            });
        }

    }
}

