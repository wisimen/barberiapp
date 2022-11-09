using AutoMapper;
using Barberiapp.DTOs.Autenticacion;
using Barberiapp.DTOs.Barberia;
using Barberiapp.DTOs.Barbero;
using Barberiapp.DTOs.Cita;
using Barberiapp.DTOs.Cliente;
using Barberiapp.DTOs.FotoCorte;
using Barberiapp.DTOs.Horario;
using Barberiapp.DTOs.MediosPago;
using Barberiapp.DTOs.Servicio;
using Barberiapp.DTOs.TipoDocumento;
using Barberiapp.DTOs.TipoServicio;
using Barberiapp.Entidades;
using Barberiapp.Models;

namespace Barberiapp.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Mapeo Autenticación
            CreateMap<UsuarioCreacionDTO, UsuarioCreacionBaseDTO>().ReverseMap();
            CreateMap<UsuarioCreacionDTO, ApplicationUser>().ReverseMap();

            //Mapeo Barberia
            CreateMap<Barberia, BarberiaDTO>().ReverseMap();
            CreateMap<BarberiaCreacionDTO, Barberia>();
            CreateMap<BarberiaActualizacionDTO, Barberia>();
            CreateMap<BarberiaDTO, Barberia>();

            //Mapeo Barbero
            CreateMap<Barbero, BarberoDTO>().ForAllMembers(x => x.MapFrom(src => src.Usuario));
            CreateMap<BarberoCreacionDTO, Barbero>();
            CreateMap<BarberoActualizacionDTO, Barbero>();
            CreateMap<BarberoDTO, Barbero>();

            //Mapeo Cliente
            CreateMap<Cliente, ClienteDTO>().ForAllMembers(x => x.MapFrom(src => src.Usuario));
            CreateMap<ClienteCreacionDTO, Cliente>();
            CreateMap<ClienteActualizacionDTO, Cliente>();
            CreateMap<ClienteDTO, Cliente>();

            //Mapeo Cita
            CreateMap<Cita, CitaDTO>().ReverseMap();
            CreateMap<CitaCreacionDTO, Cita>();
            CreateMap<CitaActualizacionDTO, Cita>();
            CreateMap<CitaDTO, Cita>();

            //Mapeo FotoCorte
            CreateMap<FotoCorte, FotoCorteDTO>().ReverseMap();
            CreateMap<FotoCorteCreacionDTO, FotoCorte>();
            CreateMap<FotoCorteActualizacionDTO, FotoCorte>();
            CreateMap<FotoCorteDTO, FotoCorte>();

            //Horario
            CreateMap<Horario, HorarioDTO>().ReverseMap();
            CreateMap<HorarioCreacionDTO, Horario>();
            CreateMap<HorarioActualizacionDTO, Horario>();
            CreateMap<HorarioDTO, Horario>();

            //MediosPago
            CreateMap<MediosPago, MediosPagoDTO>().ReverseMap();
            CreateMap<MediosPagoCreacionDTO, MediosPago>();
            CreateMap<MediosPagoActualizacionDTO, MediosPago>();
            CreateMap<MediosPagoDTO, MediosPago>();

            //Servicio
            CreateMap<Servicio, ServicioDTO>().ReverseMap();
            CreateMap<ServicioCreacionDTO, Servicio>();
            CreateMap<ServicioActualizacionDTO, Servicio>();
            CreateMap<ServicioDTO, Servicio>();

            //TipoDocumento
            CreateMap<TipoDocumento, TipoDocumentoDTO>().ReverseMap();
            CreateMap<TipoDocumentoCreacionDTO, TipoDocumento>();
            CreateMap<TipoDocumentoActualizacionDTO, TipoDocumento>();
            CreateMap<TipoDocumentoDTO, TipoDocumento>();

            //TipoServicio
            CreateMap<TipoServicio, TipoServicioDTO>().ReverseMap();
            CreateMap<TipoServicioCreacionDTO, TipoServicio>();
            CreateMap<TipoServicioActualizacionDTO, TipoServicio>();
            CreateMap<TipoServicioDTO, TipoServicio>();
        }
    }
}
