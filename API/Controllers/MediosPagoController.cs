using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.MediosPago;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediosPagopp.Controllers
{
    [ApiController]
    [Route("api/MediosPago")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class MediosPagoController : Controller
    {
        private readonly ILogger<MediosPagoController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MediosPagoController(ILogger<MediosPagoController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MediosPagoDTO>>> Get()
        {
            var list = await context.MediosPago.ToListAsync();
            var resultado = list.Select(x => mapper.Map<MediosPagoDTO>(x)).ToList();
            return resultado;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Post([FromBody] MediosPagoCreacionDTO mediosPagoCreacionDTO)
        {

            var mediosPago = mapper.Map<MediosPago>(mediosPagoCreacionDTO);
            context.Add(mediosPago);
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{codigoMediosPago}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Put(MediosPagoActualizacionDTO mediosPago, int codigoMedioPago)
        {
            if (mediosPago.CodigoMedioPago != codigoMedioPago)
            {
                return BadRequest("El codigo no es válido");
            }
            var existe = await context.MediosPago.AnyAsync(x => x.CodigoMedioPago == codigoMedioPago);
            if (!existe)
            {
                return NotFound();
            }
            var mediosPagoEntidad = mapper.Map<MediosPago>(mediosPago);
            context.Update(mediosPagoEntidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{codigoMedioPago}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Delete(int codigoMedioPago)
        {
            var mediosPago = await context.MediosPago.FirstOrDefaultAsync(x => x.CodigoMedioPago == codigoMedioPago);

            if (mediosPago == null)
            {
                return NotFound();
            }
            context.Remove(mediosPago);
            await context.SaveChangesAsync();
            logger.LogWarning("Deleted mediosPago: {@mediosPago}", mediosPago);
            return NoContent();
        }
    }
}
