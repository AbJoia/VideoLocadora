using System;
using System.Collections.Generic;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.ItemAluguel;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Enuns;

namespace src.Api.Service.Test.Aluguel
{
    public class AluguelTeste
    {
        public Guid AluguelId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid UsuarioIdAlterado { get; set; }
        public DateTime DataDevolucao { get; set; }
        public DateTime DataDevolucaoAlterado { get; set; }

        public List<AluguelDtoGetResult> alugueis = new List<AluguelDtoGetResult>();
        public AluguelDto aluguelDto;
        public AluguelDtoCompleteResult aluguelDtoCompleteResult;
        public AluguelDtoCreateResult aluguelDtoCreateResult;
        public AluguelDtoGetResult aluguelDtoGetResult;
        public AluguelDtoUpdate aluguelDtoUpdate;
        public AluguelDtoUpdateResult aluguelDtoUpdateResult;

        public AluguelTeste()
        {
            AluguelId = Guid.NewGuid();
            UsuarioId = Guid.NewGuid();
            UsuarioIdAlterado = Guid.NewGuid();
            DataDevolucao = DateTime.UtcNow.AddHours(36.0);
            DataDevolucaoAlterado = DateTime.UtcNow.AddHours(72.0);

            alugueis = GetAlugueis(new Random().Next(2, 5));

            aluguelDto = new AluguelDto
            {
                UsuarioId = UsuarioId,
                DataDevolucao = DataDevolucao
            };

            aluguelDtoCreateResult = new AluguelDtoCreateResult
            {
                AluguelId = AluguelId,                
                DataDevolucao = DataDevolucao,                
                CreateAt = DateTime.UtcNow,
                Usuario = new UsuarioDtoGetResult
                    {
                        Id = UsuarioId,
                        Email = Faker.Internet.Email(),
                        TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                        Nome = Faker.Name.FullName(),                        
                    },
            }; 

            aluguelDtoCompleteResult = new AluguelDtoCompleteResult
            {
                AluguelId = AluguelId,
                DataDevolucao = DataDevolucao,
                Itens = GetItensAluguel(new Random().Next(2, 4)),
                Usuario = new UsuarioDtoGetResult
                    {
                        Id = UsuarioId,
                        Email = Faker.Internet.Email(),
                        TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                        Nome = Faker.Name.FullName(),                        
                    },                    
            };

            aluguelDtoGetResult = new AluguelDtoGetResult
            {
                AluguelId = AluguelId,
                DataDevolucao = DataDevolucao,
                Usuario = new UsuarioDtoGetResult
                    {
                        Id = UsuarioId,
                        Email = Faker.Internet.Email(),
                        TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                        Nome = Faker.Name.FullName(),                        
                    },
            };

            aluguelDtoUpdate = new AluguelDtoUpdate
            {
                Id = AluguelId,
                DataDevolucao = DataDevolucaoAlterado,
                UsuarioId = UsuarioIdAlterado,
            };

            aluguelDtoUpdateResult = new AluguelDtoUpdateResult
            {
                AluguelId = AluguelId,
                DataDevolucao = DataDevolucaoAlterado,
                Usuario = new UsuarioDtoGetResult
                {
                    Id = UsuarioIdAlterado,
                    Email = Faker.Internet.Email(),
                    TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                    Nome = Faker.Name.FullName(),
                }
            };               
            
        }

        private List<AluguelDtoGetResult> GetAlugueis(int v)
        {
            List<AluguelDtoGetResult> list = new List<AluguelDtoGetResult>();
            for (int i = 0; i < v; i++)
            {
                list.Add(
                    new AluguelDtoGetResult
                    {
                        AluguelId = AluguelId,
                        DataDevolucao = DataDevolucao,
                        Usuario = new UsuarioDtoGetResult
                            {
                                Id = UsuarioId,
                                Email = Faker.Internet.Email(),
                                TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                                Nome = Faker.Name.FullName(),                        
                            },
                    }
                );
            }
            return list;
        }

        private IEnumerable<ItemAluguelDtoGetResult> GetItensAluguel(int v)
        {
            List<ItemAluguelDtoGetResult> list = new List<ItemAluguelDtoGetResult>();
            for(int i = 0; i < v; i++ )
            {
                list.Add(
                   new ItemAluguelDtoGetResult
                   {
                       Id = Guid.NewGuid(),
                       Filme = new FilmeDtoLocacaoResult
                        {
                            Id = Guid.NewGuid(),
                            Titulo = Faker.Country.Name(),
                            Categoria = (Categoria) new Random()
                                        .Next(Enum.GetNames(typeof(Categoria)).Length)                            
                        }
                   }     
                );
            }
            return list;
        }
    }
}