using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.TipoServicio;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TipoServiciopp.Controllers
{
    [ApiController]
    [Route("api/TipoServicio")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class TipoServicioController : Controller
    {
        private readonly ILogger<TipoServicioController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoServicioController(ILogger<TipoServicioController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoServicioDTO>>> Get()
        {
            var list = await context.TipoServicio.ToListAsync();
            var resultado = list.Select(x => mapper.Map<TipoServicioDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("{codigoTipoServicio:int}")]
        public async Task<ActionResult<TipoServicioDTO>> GetPorDia(int codigoTipoServicio)
        {
            var TipoServicio = await context.TipoServicio
                .FirstOrDefaultAsync(x => x.CodigoTipoServicio == codigoTipoServicio);
            if (TipoServicio == null)
            {
                return NotFound();
            }
            return mapper.Map<TipoServicioDTO>(TipoServicio);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoServicioCreacionDTO tipoServicioCreacionDTO)
        {
            var tipoServicio = mapper.Map<TipoServicio>(tipoServicioCreacionDTO);
            context.Add(tipoServicio);
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{codigoTipoServicio}")]
        public async Task<ActionResult> Put(TipoServicioActualizacionDTO tipoServicio, int codigoTipoServicio)
        {
            if (tipoServicio.CodigoTipoServicio != codigoTipoServicio)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.TipoServicio.AnyAsync(x => x.CodigoTipoServicio == codigoTipoServicio);
            if (!existe)
            {
                return NotFound();
            }
            var tipoServicioEntidad = mapper.Map<TipoServicio>(tipoServicio);
            context.Update(tipoServicioEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{codigoTipoServicio}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Delete(int codigoTipoServicio)
        {
            var tipoServicio = await context.TipoServicio.FirstOrDefaultAsync(x => x.CodigoTipoServicio == codigoTipoServicio);

            if (tipoServicio == null)
            {
                return NotFound();
            }
            context.Remove(tipoServicio);
            await context.SaveChangesAsync();
            logger.LogWarning("Deleted tipoServicio: {@tipoServicio}", tipoServicio);
            return NoContent();
        }
    }
}
