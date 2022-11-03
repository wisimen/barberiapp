using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.Detalle;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Controllers
{
    [ApiController]
    [Route("api/Detalle")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class DetalleController : Controller
    {
        private readonly ILogger<DetalleController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DetalleController(ILogger<DetalleController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DetalleDTO>>> Get()
        {
            var list = await context.Detalle.ToListAsync();
            var resultado = list.Select(x => mapper.Map<DetalleDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("PorProcedimiento/{codigo:int}")]
        public async Task<ActionResult<List<DetalleDTO>>> GetPorProcedimiento(int codigo)
        {
            var Detalle = await context.Detalle.Where(x => x.CodigoProcedimiento == codigo).ToListAsync();
            if (Detalle == null)
            {
                return NotFound();
            }
            return mapper.Map<List<DetalleDTO>>(Detalle);
        }

        // Búsqueda por parámetro
        [HttpGet("PorHistorial/{codigo:int}")]
        public async Task<ActionResult<List<DetalleDTO>>> GetPorHistorial(int codigo)
        {
            var Detalle = await context.Detalle.Where(x => x.CodigoHistorial == codigo).ToListAsync();
            if (Detalle == null)
            {
                return NotFound();
            }
            return mapper.Map<List<DetalleDTO>>(Detalle);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DetalleDTO detalleDTO)
        {
            var Detalle = mapper.Map<Detalle>(detalleDTO);
            context.Add(Detalle);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [HttpDelete("{codigoProcedimiento}/codigoHistorial")]
        public async Task<ActionResult> Delete(int codigoProcedimiento, int codigoHistorial)
        {
            var Detalle = await context.Detalle.FirstOrDefaultAsync(x => x.CodigoProcedimiento == codigoProcedimiento && x.CodigoHistorial == codigoHistorial);

            if (Detalle == null)
            {
                return NotFound();
            }
            context.Remove(Detalle);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }
    }
}
