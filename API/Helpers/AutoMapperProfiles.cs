using AutoMapper;
using Barberiapp.DTOs.Autenticacion;
using Barberiapp.DTOs.Detalle;
using Barberiapp.DTOs.Historial;
using Barberiapp.DTOs.ImagenVehiculo;
using Barberiapp.DTOs.Marca;
using Barberiapp.DTOs.Procedimiento;
using Barberiapp.DTOs.TipoDocumento;
using Barberiapp.DTOs.TipoVehiculo;
using Barberiapp.DTOs.Vehiculo;
using Barberiapp.Entidades;
using Barberiapp.Models;

namespace Barberiapp.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            //Mapeo de Historial
            CreateMap<Historial, HistorialDTO>().ReverseMap();
            CreateMap<HistorialCreacionDTO, Historial>();
            CreateMap<HistorialActualizacionDTO, Historial>();
            CreateMap<HistorialDTO, Historial>();

            //Mapeo de TipoVehiculo
            CreateMap<TipoVehiculo, TipoVehiculoDTO>().ReverseMap();
            CreateMap<TipoVehiculoCreacionDTO, TipoVehiculo>();
            CreateMap<TipoVehiculoActualizacionDTO, TipoVehiculo>();
            CreateMap<TipoVehiculoDTO, TipoVehiculo>();

            //Mapeo de TipoDocumento
            CreateMap<TipoDocumento, TipoDocumentoDTO>().ReverseMap();
            CreateMap<TipoDocumentoCreacionDTO, TipoDocumento>();
            CreateMap<TipoDocumentoActualizacionDTO, TipoDocumento>();
            CreateMap<TipoDocumentoDTO, TipoDocumento>();

            //Mapeo de Detalle
            CreateMap<Detalle, DetalleDTO>().ReverseMap();

            //Mapeo de Vehiculo
            CreateMap<Vehiculo, VehiculoDTO>().ReverseMap();
            CreateMap<VehiculoCreacionDTO, Vehiculo>();
            CreateMap<VehiculoActualizacionDTO, Vehiculo>();
            CreateMap<VehiculoDTO, Vehiculo>();

            //Mapeo de Marca
            CreateMap<Marca, MarcaDTO>().ReverseMap();
            CreateMap<MarcaCreacionDTO, Marca>();
            CreateMap<MarcaActualizacionDTO, Marca>();
            CreateMap<MarcaDTO, Marca>();

            //Mapeo de Procedimiento
            CreateMap<Procedimiento, ProcedimientoDTO>().ReverseMap();
            CreateMap<ProcedimientoCreacionDTO, Procedimiento>();
            CreateMap<ProcedimientoActualizacionDTO, Procedimiento>();
            CreateMap<ProcedimientoDTO, Procedimiento>();

            //Mapeo de ImagenVehiculo
            CreateMap<ImagenVehiculo, ImagenVehiculoDTO>().ReverseMap();

            //Mapeo Autenticación
            CreateMap<UsuarioCreacionDTO, UsuarioCreacionBaseDTO>().ReverseMap();
            CreateMap<UsuarioCreacionDTO, IdentityModels>().ReverseMap();

        }
    }
}
