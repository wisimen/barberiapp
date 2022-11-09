using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.Servicio;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Serviciopp.Controllers
{
    [ApiController]
    [Route("api/servicio")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class ServicioController : Controller
    {
        private readonly ILogger<ServicioController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ServicioController(ILogger<ServicioController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServicioDTO>>> Get()
        {
            var list = await context.Servicio.ToListAsync();
            var resultado = list.Select(x => mapper.Map<ServicioDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("PorBarberia/{codigoBarberia:int}")]
        public async Task<ActionResult<List<ServicioDTO>>> GetPorBarberia(int codigoBarberia)
        {
            var Servicio = await context.Servicio
                .Where(x => x.Barberia.CodigoBarberia == codigoBarberia)
                .ToListAsync();
            if (Servicio == null)
            {
                return NotFound();
            }
            return mapper.Map<List<ServicioDTO>>(Servicio);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ServicioCreacionDTO servicioCreacionDTO)
        {

            var servicio = mapper.Map<Servicio>(servicioCreacionDTO);
            context.Add(servicio);
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{codigoServicio:int}")]
        public async Task<ActionResult> Put(ServicioActualizacionDTO servicio, int codigoServicio)
        {
            if (servicio.CodigoServicio != codigoServicio)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.Servicio.AnyAsync(x => x.CodigoServicio == codigoServicio);
            if (!existe)
            {
                return NotFound();
            }
            var servicioEntidad = mapper.Map<Servicio>(servicio);
            context.Update(servicioEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{codigoServicio:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Delete(int codigoServicio)
        {
            var servicio = await context.Servicio.FirstOrDefaultAsync(x => x.CodigoServicio == codigoServicio);

            if (servicio == null)
            {
                return NotFound();
            }
            context.Remove(servicio);
            await context.SaveChangesAsync();
            logger.LogWarning("Deleted servicio: {@servicio}", servicio);
            return NoContent();
        }
    }
}
