using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.TipoDocumento;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Controllers
{
    [ApiController]
    [Route("api/tipoDocumento")]
    [AllowAnonymous]
    public class TipoDocumentoController : Controller
    {
        private readonly ILogger<TipoDocumentoController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoDocumentoController(ILogger<TipoDocumentoController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoDocumentoDTO>>> Get()
        {
            var list = await context.TipoDocumento.ToListAsync();
            var resultado = list.Select(x => mapper.Map<TipoDocumentoDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("{codigoTipoDocumento:int}")]
        public async Task<ActionResult<TipoDocumentoDTO>> Get(int codigoTipoDocumento)
        {
            var tipoDocumento = await context.TipoDocumento.FirstOrDefaultAsync(x => x.CodigoTipoDocumento == codigoTipoDocumento);
            if (tipoDocumento == null)
            {
                return NotFound();
            }
            return mapper.Map<TipoDocumentoDTO>(tipoDocumento);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoDocumentoCreacionDTO tipoDocumentoCreacionDTO)
        {
            var tipoDocumento = mapper.Map<TipoDocumento>(tipoDocumentoCreacionDTO);
            context.Add(tipoDocumento);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }

        [HttpPut("{codigoTipoDocumento:int}")]
        public async Task<ActionResult> Put(TipoDocumentoActualizacionDTO tipoDocumento, int codigoTipoDocumento)
        {
            if (tipoDocumento.CodigoTipoDocumento != codigoTipoDocumento)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.TipoDocumento.AnyAsync(x => x.CodigoTipoDocumento == codigoTipoDocumento);
            if (!existe)
            {
                return NotFound();
            }
            var tipoDocumentoEntidad = mapper.Map<TipoDocumento>(tipoDocumento);
            context.Update(tipoDocumentoEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [HttpDelete("{codigoTipoDocumento:int}")]
        public async Task<ActionResult> Delete(int codigoTipoDocumento)
        {
            var tipoDocumento = await context.TipoDocumento.FirstOrDefaultAsync(x => x.CodigoTipoDocumento == codigoTipoDocumento);

            if (tipoDocumento == null)
            {
                return NotFound();
            }
            context.Remove(tipoDocumento);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }
    }
}
