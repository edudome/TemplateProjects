using AutoMapper;
using Template.Domain.Entities;
using Template.Domain.EntitiesResult;
using Template.Application.Models.AAAs;
using Template.Application.Handlers.AAAs;
using Template.Application.Handlers.Account;
using Template.Application.Models.Usuarios;

namespace Template.Application.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AAA, AAAEntityResult>()
                //.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Apellido))
                .ReverseMap();

            CreateMap<AAA, AAAModel>()
                .ReverseMap();

            CreateMap<AAAEntityResult, AAAModel>()
                .ReverseMap();


            CreateMap<AAA, AddAAA>()
                .ReverseMap();

            CreateMap<AAA, UpdateAAA>()
                .ReverseMap();

            CreateMap<AAA, DeleteAAAById>()
                .ReverseMap();


            CreateMap<Usuario, UsuarioModel>()
                .ReverseMap();

            CreateMap<Usuario, RegisterUsuario>()
                .ReverseMap();


        }
    }
}
