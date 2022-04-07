using AutoMapper;
using JMusik.Dtos;
using JMusik.Models;

namespace JMusik.WebApi.Profiles
{
    public class JMusikProfile : Profile
    {
        public JMusikProfile()
        {
            //PRODUCTO
            this.CreateMap<Producto, ProductoDTO>().ReverseMap();
            //PERFIL
            this.CreateMap<Perfil, PerfilDTO>().ReverseMap();
            //ORDEN
            this.CreateMap<Orden, OrdenDTO>()
                .ForMember(u => u.Usuario, p => p.MapFrom(m => m.Usuario.Username))
                .ReverseMap()
                .ForMember(u => u.Usuario, p => p.Ignore());
            //DETALLES ORDEN
            this.CreateMap<DetalleOrden, DetalleOrdenDTO>()
                .ForMember(u => u.Producto, p => p.MapFrom(u => u.Producto.Nombre))
                .ReverseMap()
                .ForMember(u => u.Producto, p => p.Ignore());
            
            //USUARIOS
            this.CreateMap<Usuario, UsuarioRegistroDto>()
                .ForMember(u => u.Perfil, p => p.MapFrom(m => m.Perfil.Nombre))
                .ReverseMap()
                .ForMember(u => u.Perfil, p => p.Ignore());

            this.CreateMap<Usuario, UsuarioActualizacionDto>()
                .ReverseMap();

            this.CreateMap<Usuario, UsuarioListaDto>()
                .ForMember(u => u.Perfil, p => p.MapFrom(m => m.Perfil.Nombre))
                .ForMember(u => u.NombreCompleto, p => p.MapFrom(m => string.Format("{0} {1}",
                        m.Nombre, m.Apellidos)))
                .ReverseMap();

            this.CreateMap<Usuario, LoginModelDto>().ReverseMap();

            this.CreateMap<Usuario, PerfilUsuarioDto>().ReverseMap();

        }
    }
}
