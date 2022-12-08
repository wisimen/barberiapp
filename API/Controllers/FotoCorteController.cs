using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.FotoCorte;
using Barberiapp.Entidades;
using Barberiapp.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Controllers
{
    [ApiController]
    [Route("api/fotoCorte")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class FotoCorteController : Controller
    {
        private readonly ILogger<FotoCorteController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "LogoFotoCorteFiles";

        public FotoCorteController(ILogger<FotoCorteController> logger, ApplicationDbContext context, IMapper mapper,
             IAlmacenadorArchivos almacenadorArchivos)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<FotoCorteDTO>>> Get()
        {
            var list = await context.FotoCorte.ToListAsync();
            var resultado = list.Select(x => mapper.Map<FotoCorteDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("PorBarbero/{codigoBarbero:int}")]
        public async Task<ActionResult<List<FotoCorteDTO>>> GetPorBarbero(int codigoBarbero)
        {
            var FotoCorte = await context.FotoCorte
                .Where(x => x.CodigoBarbero == codigoBarbero)
                .ToListAsync();
            if (FotoCorte == null)
            {
                return NotFound();
            }
            return mapper.Map<List<FotoCorteDTO>>(FotoCorte);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Barberia")]
        public async Task<ActionResult> Post([FromForm] FotoCorteCreacionDTO fotoCorteCreacionDTO)
        {

            var fotoCorte = mapper.Map<FotoCorte>(fotoCorteCreacionDTO);

            if (fotoCorteCreacionDTO.FotoFile != null)
            {
                fotoCorte.URL_Foto = await almacenadorArchivos.GuardarArchivo(contenedor, fotoCorteCreacionDTO.FotoFile);
            }

            context.Add(fotoCorte);
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{codigoFotoCorte:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Barberia")]
        public async Task<ActionResult> Put([FromForm] FotoCorteActualizacionDTO fotoCorte, int codigoFotoCorte)
        {
            if (fotoCorte.CodigoFotoCorte != codigoFotoCorte)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.FotoCorte.AnyAsync(x => x.CodigoFotoCorte == codigoFotoCorte);
            if (!existe)
            {
                return NotFound();
            }
            var fotoCorteEntidad = mapper.Map<FotoCorte>(fotoCorte);

            if (fotoCorte.FotoFile != null)
            {
                fotoCorteEntidad.URL_Foto = await almacenadorArchivos.GuardarArchivo(contenedor, fotoCorte.FotoFile);
            }
            context.Update(fotoCorteEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Barberia")]
        [HttpDelete("{codigoFotoCorte:int}")]
        public async Task<ActionResult> Delete(int codigoFotoCorte)
        {
            var fotoCorte = await context.FotoCorte.FirstOrDefaultAsync(x => x.CodigoFotoCorte == codigoFotoCorte);

            if (fotoCorte == null)
            {
                return NotFound();
            }
            context.Remove(fotoCorte);
            await context.SaveChangesAsync();
            logger.LogWarning("Deleted fotoCorte: {@fotoCorte}", fotoCorte);
            return NoContent(); //204
        }
    }
}
