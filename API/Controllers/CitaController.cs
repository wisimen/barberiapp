using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.Cita;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Citapp.Controllers
{
    [ApiController]
    [Route("api/Cita")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class CitaController : Controller
    {
        private readonly ILogger<CitaController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CitaController(ILogger<CitaController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CitaDTO>>> Get()
        {
            var list = await context.Cita.ToListAsync();
            var resultado = list.Select(x => mapper.Map<CitaDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("PorDia/{dia:int}")]
        public async Task<ActionResult<List<CitaDTO>>> GetPorDia(int dia)
        {
            var Cita = await context.Cita
                .Where(x => x.Fecha.DayOfYear == dia)
                .ToListAsync();
            if (Cita == null)
            {
                return NotFound();
            }
            return mapper.Map<List<CitaDTO>>(Cita);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CitaCreacionDTO citaCreacionDTO)
        {

            var cita = mapper.Map<Cita>(citaCreacionDTO);
            context.Add(cita);
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{codigoCita}")]
        public async Task<ActionResult> Put(CitaActualizacionDTO cita, int codigoCita)
        {
            if (cita.CodigoCita != codigoCita)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.Cita.AnyAsync(x => x.CodigoCita == codigoCita);
            if (!existe)
            {
                return NotFound();
            }
            var citaEntidad = mapper.Map<Cita>(cita);
            context.Update(citaEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{codigoCita}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Delete(int codigoCita)
        {
            var cita = await context.Cita.FirstOrDefaultAsync(x => x.CodigoCita == codigoCita);

            if (cita == null)
            {
                return NotFound();
            }
            context.Remove(cita);
            await context.SaveChangesAsync();
            logger.LogWarning("Deleted cita: {@cita}", cita);
            return NoContent();
        }
    }
}
