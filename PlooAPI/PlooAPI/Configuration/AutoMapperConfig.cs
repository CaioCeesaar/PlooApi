using AutoMapper;
using PlooAPI.Models;

namespace PlooAPI.Configuration;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Usuario, UsuarioModel>().ReverseMap();
        CreateMap<Usuario, UsuarioUpdateModel>().ReverseMap();
        CreateMap<Perfil, PerfilModel>().ReverseMap();
        CreateMap<Perfil,PerfilUpdateModel>().ReverseMap();
    }
}