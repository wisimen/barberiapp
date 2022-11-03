using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.Procedimiento;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Controllers
{
    [ApiController]
    [Route("api/Procedimiento")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class ProcedimientoController : Controller
    {
        private readonly ILogger<ProcedimientoController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProcedimientoController(ILogger<ProcedimientoController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProcedimientoDTO>>> Get()
        {
            var list = await context.Procedimiento.ToListAsync();
            var resultado = list.Select(x => mapper.Map<ProcedimientoDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("{codigo:int}")]
        public async Task<ActionResult<ProcedimientoDTO>> Get(int codigo)
        {
            var procedimiento = await context.Procedimiento.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (procedimiento == null)
            {
                return NotFound();
            }
            return mapper.Map<ProcedimientoDTO>(procedimiento);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProcedimientoCreacionDTO procedimientoCreacionDTO)
        {
            var procedimiento = mapper.Map<Procedimiento>(procedimientoCreacionDTO);
            context.Add(procedimiento);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult> Put(ProcedimientoActualizacionDTO procedimiento, int codigo)
        {
            if (procedimiento.Codigo != codigo)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.Procedimiento.AnyAsync(x => x.Codigo == codigo);
            if (!existe)
            {
                return NotFound();
            }
            var procedimientoEntidad = mapper.Map<Procedimiento>(procedimiento);
            context.Update(procedimientoEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{codigo}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Delete(int codigo)
        {
            var procedimiento = await context.Procedimiento.FirstOrDefaultAsync(x => x.Codigo == codigo);

            if (procedimiento == null)
            {
                return NotFound();
            }
            context.Remove(procedimiento);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }
    }
}
