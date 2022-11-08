using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.Barberia;
using Barberiapp.Entidades;
using Barberiapp.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Controllers
{
    [ApiController]
    [Route("api/Barberia")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class BarberiaController : Controller
    {
        private readonly ILogger<BarberiaController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "LogoBarberiaFiles";

        public BarberiaController(ILogger<BarberiaController> logger, ApplicationDbContext context, IMapper mapper,
             IAlmacenadorArchivos almacenadorArchivos)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<BarberiaDTO>>> Get()
        {
            var list = await context.Barberia.ToListAsync();
            var resultado = list.Select(x => mapper.Map<BarberiaDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("PorNombre/{nombre:string}")]
        public async Task<ActionResult<List<BarberiaDTO>>> GetPorNombre(string nombre)
        {
            var Barberia = await context.Barberia
                .Where(x => x.Nombre
                    .Contains(
                        nombre,
                        StringComparison.InvariantCultureIgnoreCase)
                    )
                    .ToListAsync();
            if (Barberia == null)
            {
                return NotFound();
            }
            return mapper.Map<List<BarberiaDTO>>(Barberia);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Post([FromBody] BarberiaCreacionDTO barberiaCreacionDTO)
        {

            var barberia = mapper.Map<Barberia>(barberiaCreacionDTO);
            if (barberiaCreacionDTO.LogoFile != null)
            {
                barberia.Logo = await almacenadorArchivos.GuardarArchivo(contenedor, barberiaCreacionDTO.LogoFile);
            }

            context.Add(barberia);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }


        [HttpPut("{codigoBarberia}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Put(BarberiaActualizacionDTO barberia, int codigoBarberia)
        {
            if (barberia.CodigoBarberia != codigoBarberia)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.Barberia.AnyAsync(x => x.CodigoBarberia == codigoBarberia);
            if (!existe)
            {
                return NotFound();
            }
            var barberiaEntidad = mapper.Map<Barberia>(barberia);

            if (barberia.LogoFile != null)
            {
                barberiaEntidad.Logo = await almacenadorArchivos.GuardarArchivo(contenedor, barberia.LogoFile);
            }
            context.Update(barberiaEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [HttpDelete("{codigoBarberia}")]
        public async Task<ActionResult> Delete(int codigoBarberia)
        {
            var barberia = await context.Barberia.FirstOrDefaultAsync(x => x.CodigoBarberia == codigoBarberia);

            if (barberia == null)
            {
                return NotFound();
            }
            context.Remove(barberia);
            await context.SaveChangesAsync();
            logger.LogWarning("Deleted barberia: {@barberia}", barberia);
            return NoContent(); //204
        }
    }
}
