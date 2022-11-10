using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.Autenticacion;
using Barberiapp.DTOs.Cliente;
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
    [Route("api/cliente")]
    [AllowAnonymous]
    public class ClienteController : CuentasController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<CuentasController> logger;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "FotoClienteFiles";

        public ClienteController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            IMapper mapper,
            ILogger<ClienteController> logger,
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Barberia")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]

        public async Task<ActionResult<List<ClienteDTO>>> Get()
        {
            var list = await context.Cliente.ToListAsync();
            var resultado = list.Select(x => mapper.Map<ClienteDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("PorNombre/{nombre}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Barberia")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<List<ClienteDTO>>> GetPorNombre(string nombre)
        {
            var Cliente = await context.Cliente
                .Where(
                    x => x.Usuario.Nombre
                    .Contains(
                        nombre,
                        StringComparison.InvariantCultureIgnoreCase)
                    )
                    .ToListAsync();
            if (Cliente == null)
            {
                return NotFound();
            }
            return mapper.Map<List<ClienteDTO>>(Cliente);
        }

        // Búsqueda por codigoCliente
        [HttpGet("{codigoCliente:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Barberia")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<ClienteDTO>> GetPorCodigoCliente(int codigoCliente)
        {
            var Cliente = await context.Cliente
                .FirstOrDefaultAsync(x => x.CodigoCliente == codigoCliente);
            if (Cliente == null)
            {
                return NotFound();
            }
            return mapper.Map<ClienteDTO>(Cliente);
        }

        [HttpPost]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar([FromForm] ClienteCreacionDTO clienteCreacionDTO)
        {

            var clienteEntidad = mapper.Map<ApplicationUser>(clienteCreacionDTO);
            var existUser = await userManager.FindByEmailAsync(clienteEntidad.Email);
            IdentityResult resultado;
            if (existUser != null)
            {
                if (context.Cliente.FirstOrDefault(x => x.CodigoUsuario == existUser.Id) != null)
                {
                    return BadRequest($"Ya existe un cliente registrado con la cuenta ${clienteEntidad.Email}");
                }
                clienteEntidad.Id = existUser.Id;
                resultado = await userManager.UpdateAsync(clienteEntidad);
            }
            else
            {
                resultado = await NuevaCuenta(clienteCreacionDTO, clienteEntidad);
            }

            context.Add(new Cliente
            {
                CodigoUsuario = clienteEntidad.Id
            });

            await context.SaveChangesAsync();

            logger.LogWarning("Nueva cuenta creada: {@clienteEntidad.Id}", clienteEntidad.Id);
            await AddClaimsToUser(clienteCreacionDTO.Email, "Rol", "Cliente", configuration["permissionKey"]);
            if (resultado.Succeeded)
            {
                CredencialesUsuario credencial = new CredencialesUsuario
                {
                    Email = clienteCreacionDTO.Email,
                    Password = clienteCreacionDTO.Password
                };
                return await ConstruirToken(credencial);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }


        [HttpPut("{codigoCliente:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
        public async Task<ActionResult> Put([FromBody] ClienteActualizacionDTO clienteActualizacionDTO, int codigoCliente)
        {
            // Realizar validaciones
            if (clienteActualizacionDTO.CodigoCliente != codigoCliente)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.Cliente.AnyAsync(x => x.CodigoCliente == codigoCliente);
            if (!existe)
            {
                return NotFound();
            }
            await ActualizarCliente(clienteActualizacionDTO);

            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [HttpDelete("{codigoCliente:int}")]
        public async Task<ActionResult> Delete(int codigoCliente)
        {
            var cliente = await context.Cliente.FirstOrDefaultAsync(x => x.CodigoCliente == codigoCliente);

            if (cliente == null)
            {
                return NotFound();
            }
            await userManager.DeleteAsync(cliente.Usuario);
            logger.LogWarning("Deleted cliente: {@cliente}", cliente);
            return NoContent();
        }

        private async Task ActualizarCliente(ClienteActualizacionDTO clienteActualizacionDTO)
        {
            if (clienteActualizacionDTO.Password != clienteActualizacionDTO.NuevoPassword)
            {
                await ActualizarPassword(clienteActualizacionDTO);
                clienteActualizacionDTO.Password = clienteActualizacionDTO.NuevoPassword;
            }

            var clienteEntidad = mapper.Map<ApplicationUser>(clienteActualizacionDTO);

            if (clienteActualizacionDTO.FotoFile != null)
            {
                clienteEntidad.Foto = await almacenadorArchivos.GuardarArchivo(contenedor, clienteActualizacionDTO.FotoFile);
            }

            await userManager.UpdateAsync(clienteEntidad);
        }

        private async Task ActualizarPassword(ClienteActualizacionDTO cliente)
        {
            var resultado = await signInManager.PasswordSignInAsync(
                    cliente.Email,
                    cliente.Password,
                    isPersistent: false,
                    lockoutOnFailure: false
                );
            if (resultado.Succeeded)
            {
                var usuario = await userManager.FindByEmailAsync(cliente.Email);
                var token = await userManager.GeneratePasswordResetTokenAsync(usuario);
                var resultadoCambio = await userManager.ResetPasswordAsync(usuario, token, cliente.NuevoPassword);
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
        private async Task<IdentityResult> NuevaCuenta(ClienteCreacionDTO clienteCreacionDTO, ApplicationUser clienteEntidad)
        {
            clienteEntidad.UserName = clienteEntidad.Email;
            clienteEntidad.Id = Guid.NewGuid().ToString();
            if (clienteCreacionDTO.FotoFile != null)
            {
                clienteEntidad.Foto = await almacenadorArchivos.GuardarArchivo(contenedor, clienteCreacionDTO.FotoFile);
            }

            return await userManager.CreateAsync(clienteEntidad, clienteCreacionDTO.Password);
        }
    }
}

