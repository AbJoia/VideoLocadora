using AutoMapper;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.ItemAluguel;
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
            CreateMap<UsuarioEntity, UsuarioDtoUpdate>()
                    .ReverseMap();        
            CreateMap<UsuarioEntity, UsuarioDtoCreateResult>()
                    .ReverseMap();
            CreateMap<UsuarioEntity, UsuarioDtoUpdateResult>()
                    .ReverseMap();
            CreateMap<UsuarioEntity, UsuarioDtoGetResult>()
                    .ReverseMap();
            CreateMap<UsuarioEntity, UsuarioDtoLocacaoResult>()
                    .ReverseMap();        

            //Funcionario Mapping
            CreateMap<FuncionarioEntity, FuncionarioDto>()
                    .ReverseMap();
            CreateMap<FuncionarioEntity, FuncionarioDtoUpdate>()
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
            CreateMap<FilmeEntity, FilmeDtoUpdate>()
                    .ReverseMap();            
            CreateMap<FilmeEntity, FilmeDtoCreateResult>()
                    .ReverseMap();
            CreateMap<FilmeEntity, FilmeDtoGetResult>()
                    .ReverseMap();
            CreateMap<FilmeEntity, FilmeDtoUpdateResult>()
                    .ReverseMap();
            CreateMap<FilmeEntity, FilmeDtoLocacaoResult>()
                    .ReverseMap();
        
            //Aluguel Mapping
            CreateMap<AluguelEntity, AluguelDtoCreateResult>()
                    .ReverseMap();
            CreateMap<AluguelEntity, AluguelDtoGetResult>()
                    .ReverseMap();            
            CreateMap<AluguelEntity, AluguelDtoUpdateResult>()
                    .ReverseMap();
            CreateMap<AluguelEntity, AluguelDtoCompleteResult>()
                    .ReverseMap();    
        
            //ItemAluguel Mapping
            CreateMap<ItemAluguelEntity, ItemAluguelDtoCreateResult>()
                    .ReverseMap();
            CreateMap<ItemAluguelEntity, ItemAluguelDtoGetResult>()
                    .ReverseMap();            
            CreateMap<ItemAluguelEntity, ItemAluguelDtoUpdateResult>()
                    .ReverseMap();     
        }        
    }
}
