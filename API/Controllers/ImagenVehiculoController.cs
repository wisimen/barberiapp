using AutoMapper;
using Barberiapp.Data;
using Barberiapp.DTOs.ImagenVehiculo;
using Barberiapp.Entidades;
using Barberiapp.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barberiapp.Controllers
{
    [ApiController]
    [Route("api/ImagenVehiculo")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "BasicUser")]
    public class ImagenVehiculoController : Controller
    {
        private readonly ILogger<ImagenVehiculoController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "ImagenVehiculoFiles";

        public ImagenVehiculoController(ILogger<ImagenVehiculoController> logger, ApplicationDbContext context, IMapper mapper,
             IAlmacenadorArchivos almacenadorArchivos)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ImagenVehiculoDTO>>> Get()
        {
            var entidades = await context.ImagenVehiculo.ToListAsync();
            return mapper.Map<List<ImagenVehiculoDTO>>(entidades);
        }

        [HttpGet("{codigo:int}")]
        public async Task<ActionResult<ImagenVehiculoDTO>> Get(int codigo)
        {
            var imagenVehiculo = await context.ImagenVehiculo.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (imagenVehiculo == null)
            {


                return NotFound();
            }
            return mapper.Map<ImagenVehiculoDTO>(imagenVehiculo);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ImagenVehiculoCreacionArchivoDTO imagenVehiculoCreacionArchivoDTO)
        {
            var imagenVehiculo = new ImagenVehiculo();

            if (imagenVehiculoCreacionArchivoDTO.Imagen != null)
            {
                imagenVehiculo.Ruta = await almacenadorArchivos.GuardarArchivo(contenedor, imagenVehiculoCreacionArchivoDTO.Imagen);
                imagenVehiculo.Nombre = imagenVehiculoCreacionArchivoDTO.Imagen.FileName;
                imagenVehiculo.CodigoVehiculo = imagenVehiculoCreacionArchivoDTO.CodigoVehiculo;
            }
            context.Add(imagenVehiculo);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{codigo}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> Delete(int codigo)
        {
            var imagenVehiculo = await context.ImagenVehiculo.FirstOrDefaultAsync(x => x.Codigo == codigo);

            if (imagenVehiculo == null)
            {

                return NotFound();
            }

            context.Remove(imagenVehiculo);
            await context.SaveChangesAsync();
            logger.LogWarning("Deleted imagenVehiculo: {@imagenVehiculo}", imagenVehiculo);
            return NoContent(); //204
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult> Put([FromForm] ImagenVehiculoActualizacionArchivoDTO imagenVehiculoActualizacionArchivo, int codigo)
        {

            if (imagenVehiculoActualizacionArchivo.Codigo != codigo)
            {
                return BadRequest("El codigo no es válido");
            }
            var imagenVehiculo = await context.ImagenVehiculo.FirstOrDefaultAsync(x => x.Codigo == codigo);

            if (imagenVehiculo == null)
            {
                return NotFound();
            }

            if (imagenVehiculoActualizacionArchivo.Imagen != null)
            {
                imagenVehiculo.Ruta = await almacenadorArchivos.GuardarArchivo(contenedor, imagenVehiculoActualizacionArchivo.Imagen);
                imagenVehiculo.Nombre = imagenVehiculoActualizacionArchivo.Imagen.FileName;
                imagenVehiculo.CodigoVehiculo = imagenVehiculoActualizacionArchivo.CodigoVehiculo;
            }
            context.Update(imagenVehiculo);
            await context.SaveChangesAsync();

            return NoContent();
        }

    }
}
