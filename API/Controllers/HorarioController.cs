using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.Horario;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Horariopp.Controllers
{
    [ApiController]
    [Route("api/horario")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class HorarioController : Controller
    {
        private readonly ILogger<HorarioController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public HorarioController(ILogger<HorarioController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<HorarioDTO>>> Get()
        {
            var list = await context.Horario.ToListAsync();
            var resultado = list.Select(x => mapper.Map<HorarioDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("PorHora/{hora}")]
        public async Task<ActionResult<List<HorarioDTO>>> GetPorHora(string hora)
        {
            TimeSpan time = TimeSpan.Parse(hora);
            var Horario = await context.Horario
                .Where(x => x.HoraInicio <= time && x.HoraFin >= time)
                .ToListAsync();
            if (Horario == null)
            {
                return NotFound();
            }
            return mapper.Map<List<HorarioDTO>>(Horario);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Post([FromBody] HorarioCreacionDTO horarioCreacionDTO)
        {

            var horario = mapper.Map<Horario>(horarioCreacionDTO);
            context.Add(horario);
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{codigoHorario:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Put(HorarioActualizacionDTO horario, int codigoHorario)
        {
            if (horario.CodigoHorario != codigoHorario)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.Horario.AnyAsync(x => x.CodigoHorario == codigoHorario);
            if (!existe)
            {
                return NotFound();
            }
            var horarioEntidad = mapper.Map<Horario>(horario);
            context.Update(horarioEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{codigoHorario:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Delete(int codigoHorario)
        {
            var horario = await context.Horario.FirstOrDefaultAsync(x => x.CodigoHorario == codigoHorario);

            if (horario == null)
            {
                return NotFound();
            }
            context.Remove(horario);
            await context.SaveChangesAsync();
            logger.LogWarning("Deleted horario: {@horario}", horario);
            return NoContent();
        }
    }
}
