using API.Dtos;
using AutoMapper;
using Core.Entidades;

namespace API.Perfiles;

    public class MappingPerfiles: Profile
    {
        public MappingPerfiles()
        {
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Marca, MarcaDto>().ReverseMap();
            CreateMap<Categoria, CategoriaDto>().ReverseMap();

            CreateMap<Producto, ProductoListaDto>()
                .ForMember(d => d.Marca, origen => origen.MapFrom(origen => origen.Marca.Nombre))
                .ForMember(d => d.Categoria, origen => origen.MapFrom(origen => origen.Categoria.Nombre))
                .ReverseMap()
                .ForMember(origen => origen.Marca, dest => dest.Ignore())
                .ForMember(origen => origen.Categoria, dest => dest.Ignore());

            CreateMap<Producto, ProductoAddActualizarDto>()
                .ReverseMap()
                .ForMember(origen => origen.Marca, d => d.Ignore())
                .ForMember(origen => origen.Categoria, d => d.Ignore());
        }
    }
