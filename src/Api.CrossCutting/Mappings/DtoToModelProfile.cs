using AutoMapper;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Model;

namespace src.Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            //Usuario Mapping
            CreateMap<UsuarioModel, UsuarioDto>()
                    .ReverseMap();            

            //Funcionario Mapping
            CreateMap<FuncionarioModel, FuncionarioDto>()
                    .ReverseMap();           
            
            //Filme Mapping
            CreateMap<FilmeModel, FilmeDto>()
                    .ReverseMap();          
        }        
    }
}
