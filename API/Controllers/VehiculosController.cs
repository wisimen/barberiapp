using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.Vehiculo;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Controllers
{
    [ApiController]
    [Route("api/Vehiculo")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class VehiculosController : Controller
    {
        private readonly ILogger<VehiculosController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VehiculosController(ILogger<VehiculosController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehiculoDTO>>> Get()
        {
            var list = await context.Vehiculo.ToListAsync();
            var resultado = list.Select(x => mapper.Map<VehiculoDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("{codigo:int}")]
        public async Task<ActionResult<VehiculoDTO>> Get(int codigo)
        {
            var vehiculo = await context.Vehiculo.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (vehiculo == null)
            {
                return NotFound();
            }
            return mapper.Map<VehiculoDTO>(vehiculo);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VehiculoCreacionDTO vehiculoCreacionDTO)
        {
            var vehiculo = mapper.Map<Vehiculo>(vehiculoCreacionDTO);
            context.Add(vehiculo);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult> Put(VehiculoActualizacionDTO vehiculo, int codigo)
        {
            if (vehiculo.Codigo != codigo)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.Vehiculo.AnyAsync(x => x.Codigo == codigo);
            if (!existe)
            {
                return NotFound();
            }
            var vehiculoEntidad = mapper.Map<Vehiculo>(vehiculo);
            context.Update(vehiculoEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{codigo}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Delete(int codigo)
        {
            var vehiculo = await context.Vehiculo.FirstOrDefaultAsync(x => x.Codigo == codigo);

            if (vehiculo == null)
            {
                return NotFound();
            }
            context.Remove(vehiculo);
            await context.SaveChangesAsync();
            logger.LogWarning("Deleted vehiculo: {@vehiculo}", vehiculo);
            return NoContent(); //204
        }
    }
}
