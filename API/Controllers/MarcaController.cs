using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.Marca;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Controllers
{
    [ApiController]
    [Route("api/Marca")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class MarcaController : Controller
    {
        private readonly ILogger<MarcaController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MarcaController(ILogger<MarcaController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MarcaDTO>>> Get()
        {
            var list = await context.Marca.ToListAsync();
            var resultado = list.Select(x => mapper.Map<MarcaDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("{codigo:int}")]
        public async Task<ActionResult<MarcaDTO>> Get(int codigo)
        {
            var marca = await context.Marca.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (marca == null)
            {
                return NotFound();
            }
            return mapper.Map<MarcaDTO>(marca);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MarcaCreacionDTO marcaCreacionDTO)
        {
            var marca = mapper.Map<Marca>(marcaCreacionDTO);
            context.Add(marca);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult> Put(MarcaActualizacionDTO marca, int codigo)
        {
            if (marca.Codigo != codigo)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.Marca.AnyAsync(x => x.Codigo == codigo);
            if (!existe)
            {
                return NotFound();
            }
            var marcaEntidad = mapper.Map<Marca>(marca);
            context.Update(marcaEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [HttpDelete("{codigo}")]
        public async Task<ActionResult> Delete(int codigo)
        {
            var marca = await context.Marca.FirstOrDefaultAsync(x => x.Codigo == codigo);

            if (marca == null)
            {
                return NotFound();
            }
            context.Remove(marca);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }
    }
}
