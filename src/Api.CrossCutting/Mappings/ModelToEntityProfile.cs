using AutoMapper;
using src.Api.Domain.Entities;
using src.Api.Domain.Model;

namespace src.Api.CrossCutting.Mappings
{
    public class ModelToEntityProfile :Profile
    {
        public ModelToEntityProfile()
        {
            //Usuario Mapping
            CreateMap<UsuarioModel, UsuarioEntity>()
                    .ReverseMap();
            
            //Funcionario Mapping
            CreateMap<FuncionarioModel, FuncionarioEntity>()
                    .ReverseMap();

            //Filme Mapping
            CreateMap<FilmeModel, FilmeEntity>()
                    .ReverseMap();
            
            //Aluguel Mapping
            CreateMap<AluguelModel, AluguelEntity>()
                    .ReverseMap();
            
            //ItemAluguel Mapping
            CreateMap<ItemAluguelModel, ItemAluguelEntity>()
                    .ReverseMap();
            
        }
    }
}
