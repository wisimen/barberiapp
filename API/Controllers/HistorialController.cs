using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.Historial;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Controllers
{
    [ApiController]
    [Route("api/Historial")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class HistorialController : Controller
    {
        private readonly ILogger<HistorialController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public HistorialController(ILogger<HistorialController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<HistorialDTO>>> Get()
        {
            var list = await context.Historial.ToListAsync();
            var resultado = list.Select(x => mapper.Map<HistorialDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("{codigo:int}")]
        public async Task<ActionResult<HistorialDTO>> Get(int codigo)
        {
            var historial = await context.Historial.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (historial == null)
            {
                return NotFound();
            }
            return mapper.Map<HistorialDTO>(historial);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] HistorialCreacionDTO historialCreacionDTO)
        {
            var historial = mapper.Map<Historial>(historialCreacionDTO);
            context.Add(historial);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult> Put(HistorialActualizacionDTO historial, int codigo)
        {
            if (historial.Codigo != codigo)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.Historial.AnyAsync(x => x.Codigo == codigo);
            if (!existe)
            {
                return NotFound();
            }
            var historialEntidad = mapper.Map<Historial>(historial);
            context.Update(historialEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [HttpDelete("{codigo}")]
        public async Task<ActionResult> Delete(int codigo)
        {
            var historial = await context.Historial.FirstOrDefaultAsync(x => x.Codigo == codigo);

            if (historial == null)
            {
                return NotFound();
            }
            context.Remove(historial);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }
    }
}
