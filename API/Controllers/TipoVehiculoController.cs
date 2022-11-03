using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.TipoVehiculo;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Controllers
{
    [ApiController]
    [Route("api/TipoVehiculo")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class TipoVehiculoController : Controller
    {
        private readonly ILogger<TipoVehiculoController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoVehiculoController(ILogger<TipoVehiculoController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoVehiculoDTO>>> Get()
        {
            var list = await context.TipoVehiculo.ToListAsync();
            var resultado = list.Select(x => mapper.Map<TipoVehiculoDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("{codigo:int}")]
        public async Task<ActionResult<TipoVehiculoDTO>> Get(int codigo)
        {
            var tipoVehiculo = await context.TipoVehiculo.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }
            return mapper.Map<TipoVehiculoDTO>(tipoVehiculo);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoVehiculoCreacionDTO tipoVehiculoCreacionDTO)
        {
            var tipoVehiculo = mapper.Map<TipoVehiculo>(tipoVehiculoCreacionDTO);
            context.Add(tipoVehiculo);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult> Put(TipoVehiculoActualizacionDTO tipoVehiculo, int codigo)
        {
            if (tipoVehiculo.Codigo != codigo)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.TipoVehiculo.AnyAsync(x => x.Codigo == codigo);
            if (!existe)
            {
                return NotFound();
            }
            var tipoVehiculoEntidad = mapper.Map<TipoVehiculo>(tipoVehiculo);
            context.Update(tipoVehiculoEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{codigo}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Delete(int codigo)
        {
            var tipoVehiculo = await context.TipoVehiculo.FirstOrDefaultAsync(x => x.Codigo == codigo);

            if (tipoVehiculo == null)
            {
                return NotFound();
            }
            context.Remove(tipoVehiculo);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }
    }
}
