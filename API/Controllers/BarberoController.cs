using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.Autenticacion;
using Barberiapp.DTOs.Barbero;
using Barberiapp.Entidades;
using Barberiapp.Models;
using Barberiapp.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Controllers
{
    [ApiController]
    [Route("api/barbero")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class BarberoController : CuentasController

    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<CuentasController> logger;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "FotoBarberoFiles";

        public BarberoController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            IMapper mapper,
            ILogger<BarberoController> logger,
            IAlmacenadorArchivos almacenadorArchivos) : base(userManager, configuration, signInManager, context, mapper, logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<BarberoDTO>>> Get()
        {
            var list = await context.Barbero.ToListAsync();
            var resultado = list.Select(x => mapper.Map<BarberoDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("PorNombre/{nombre}")]
        public async Task<ActionResult<List<BarberoDTO>>> GetPorNombre(string nombre)
        {
            var Barbero = await context.Barbero
                .Where(
                    x => x.Usuario.Nombre
                    .Contains(
                        nombre,
                        StringComparison.InvariantCultureIgnoreCase)
                    )
                    .ToListAsync();
            if (Barbero == null)
            {
                return NotFound();
            }
            return mapper.Map<List<BarberoDTO>>(Barbero);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Barberia")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar([FromForm] BarberoCreacionDTO barberoCreacionDTO)
        {

            var barberoEntidad = mapper.Map<ApplicationUser>(barberoCreacionDTO);
            var existUser = await userManager.FindByEmailAsync(barberoEntidad.Email);
            IdentityResult resultado;
            if (existUser != null)
            {
                if (context.Barbero.FirstOrDefault(x => x.CodigoUsuario == existUser.Id) != null)
                {
                    return BadRequest($"Ya existe un barbero registrado con la cuenta ${barberoEntidad.Email}");
                }
                barberoEntidad.Id = existUser.Id;
                resultado = await userManager.UpdateAsync(barberoEntidad);
            }
            else
            {
                barberoEntidad.UserName = barberoEntidad.Email;
                barberoEntidad.Id = Guid.NewGuid().ToString();
                if (barberoCreacionDTO.FotoFile != null)
                {
                    barberoEntidad.Foto = await almacenadorArchivos.GuardarArchivo(contenedor, barberoCreacionDTO.FotoFile);
                }

                resultado = await userManager.CreateAsync(barberoEntidad, barberoCreacionDTO.Password);
            }

            context.Add(new Barbero
            {
                CodigoUsuario = barberoEntidad.Id
            });

            await context.SaveChangesAsync();

            logger.LogWarning("Nueva cuenta creada: {@barberoEntidad.Id}", barberoEntidad.Id);
            await AddClaimsToUser(barberoCreacionDTO.Email, "Rol", "Barbero", configuration["permissionKey"]);
            if (resultado.Succeeded)
            {
                CredencialesUsuario credencial = new CredencialesUsuario
                {
                    Email = barberoCreacionDTO.Email,
                    Password = barberoCreacionDTO.Password
                };
                return await ConstruirToken(credencial);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        [HttpPut("{codigoBarbero:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Barberia")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Put([FromBody] BarberoActualizacionDTO barberoActualizacionDTO, int codigoBarbero)
        {
            // Realizar validaciones
            if (barberoActualizacionDTO.CodigoBarbero != codigoBarbero)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.Barbero.AnyAsync(x => x.CodigoBarbero == codigoBarbero);
            if (!existe)
            {
                return NotFound();
            }
            await ActualizarBarbero(barberoActualizacionDTO);

            return Ok();
        }

        [HttpDelete("{codigoBarbero:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Barberia")]
        public async Task<ActionResult> Delete(int codigoBarbero)
        {
            var barbero = await context.Barbero.FirstOrDefaultAsync(x => x.CodigoBarbero == codigoBarbero);

            if (barbero == null)
            {
                return NotFound();
            }
            await userManager.DeleteAsync(barbero.Usuario);
            logger.LogWarning("Deleted barbero: {@barbero}", barbero);
            return NoContent();
        }

        private async Task ActualizarBarbero(BarberoActualizacionDTO barberoActualizacionDTO)
        {
            if (barberoActualizacionDTO.Password != barberoActualizacionDTO.NuevoPassword)
            {
                await ActualizarPassword(barberoActualizacionDTO);
                barberoActualizacionDTO.Password = barberoActualizacionDTO.NuevoPassword;
            }

            var barberoEntidad = mapper.Map<ApplicationUser>(barberoActualizacionDTO);

            if (barberoActualizacionDTO.FotoFile != null)
            {
                barberoEntidad.Foto = await almacenadorArchivos.GuardarArchivo(contenedor, barberoActualizacionDTO.FotoFile);
            }

            await userManager.UpdateAsync(barberoEntidad);
        }

        private async Task ActualizarPassword(BarberoActualizacionDTO barbero)
        {
            var resultado = await signInManager.PasswordSignInAsync(
                    barbero.Email,
                    barbero.Password,
                    isPersistent: false,
                    lockoutOnFailure: false
                );
            if (resultado.Succeeded)
            {
                var usuario = await userManager.FindByEmailAsync(barbero.Email);
                var token = await userManager.GeneratePasswordResetTokenAsync(usuario);
                var resultadoCambio = await userManager.ResetPasswordAsync(usuario, token, barbero.NuevoPassword);
                if (resultadoCambio.Succeeded)
                {
                    await signInManager.SignInAsync(usuario, isPersistent: false);
                }
                else
                {
                    throw new ApplicationException("No se pudo cambiar la contraseña");
                }
            }
            else
            {
                throw new ApplicationException("No se pudo cambiar la contraseña");
            }
        }
    }
}

