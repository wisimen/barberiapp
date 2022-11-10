using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.ServiciosCita;
using Barberiapp.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ServiciosCitapp.Controllers
{
    [ApiController]
    [Route("api/serviciosCita")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class ServiciosCitaController : Controller
    {
        private readonly ILogger<ServiciosCitaController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ServiciosCitaController(ILogger<ServiciosCitaController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiciosCitaDTO>>> Get()
        {
            var list = await context.ServiciosCita.ToListAsync();
            var resultado = list.Select(x => mapper.Map<ServiciosCitaDTO>(x)).ToList();
            return resultado;
        }

        // Búsqueda por parámetro
        [HttpGet("PorCita/{codigoCita}")]
        public async Task<ActionResult<List<ServiciosCitaDTO>>> GetPorCita(int codigoCita)
        {
            var ServiciosCita = await context.ServiciosCita
                .Where(x => x.CodigoCita == codigoCita)
                .ToListAsync();
            if (ServiciosCita == null)
            {
                return NotFound();
            }
            return mapper.Map<List<ServiciosCitaDTO>>(ServiciosCita);
        }

        // Búsqueda por parámetro
        [HttpGet("PorServicio/{codigoServicio}")]
        public async Task<ActionResult<List<ServiciosCitaDTO>>> GetPorServicio(int codigoServicio)
        {
            var ServiciosCita = await context.ServiciosCita
                .Where(x => x.CodigoServicio == codigoServicio)
                .ToListAsync();
            if (ServiciosCita == null)
            {
                return NotFound();
            }
            return mapper.Map<List<ServiciosCitaDTO>>(ServiciosCita);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ServiciosCitaDTO serviciosCitaDTO)
        {

            var serviciosCita = mapper.Map<ServiciosCita>(serviciosCitaDTO);
            context.Add(serviciosCita);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{codigoCita:int}/{codigoServicio:int}")]
        public async Task<ActionResult> Delete(int codigoCita, int codigoServicio)
        {
            var serviciosCita = await context.ServiciosCita
                .FirstOrDefaultAsync(x => x.CodigoCita == codigoCita && x.CodigoServicio == codigoServicio);

            if (serviciosCita == null)
            {
                return NotFound();
            }
            context.Remove(serviciosCita);
            await context.SaveChangesAsync();
            logger.LogWarning("Deleted serviciosCita: {@serviciosCita}", serviciosCita);
            return NoContent();
        }
    }
}
