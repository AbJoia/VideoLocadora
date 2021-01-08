using AutoMapper;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Entities;

namespace src.Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            //Usuario Mapping
            CreateMap<UsuarioEntity, UsuarioDto>()
                    .ReverseMap();
            CreateMap<UsuarioEntity, UsuarioDtoCreateResult>()
                    .ReverseMap();
            CreateMap<UsuarioEntity, UsuarioDtoUpdateResult>()
                    .ReverseMap();
            CreateMap<UsuarioEntity, UsuarioDtoGetResult>()
                    .ReverseMap();

            //Funcionario Mapping
            CreateMap<FuncionarioEntity, FuncionarioDto>()
                    .ReverseMap();
            CreateMap<FuncionarioEntity, FuncionarioDtoCreateResult>()
                    .ReverseMap();
            CreateMap<FuncionarioEntity, FuncionarioDtoGetResult>()
                    .ReverseMap();
            CreateMap<FuncionarioEntity, FuncionarioDtoUpdateResult>()
                    .ReverseMap();

            //Filme Mapping
            CreateMap<FilmeEntity, FilmeDto>()
                    .ReverseMap();
            CreateMap<FilmeEntity, FilmeDtoCreateResult>()
                    .ReverseMap();
            CreateMap<FilmeEntity, FilmeDtoGetResult>()
                    .ReverseMap();
            CreateMap<FilmeEntity, FilmeDtoUpdateResult>()
                    .ReverseMap();            
        }        
    }
}
