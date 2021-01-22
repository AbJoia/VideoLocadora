using System;
using System.Collections.Generic;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.ItemAluguel;
using src.Api.Domain.Enuns;

namespace src.Api.Service.Test.ItemAluguel
{
    public class ItemAluguelTeste 
    {
        public Guid ItemAluguelId { get; set; }
        public Guid AluguelId { get; set; }
        public Guid FilmeId { get; set; }
        public Guid FilmeIdAlterado { get; set; }
        public Guid AluguelIdAlterado { get; set; }
        public FilmeDtoLocacaoResult FilmeOriginal { get; set; }

        public List<ItemAluguelDtoGetResult> listItem = new List<ItemAluguelDtoGetResult>();
        public ItemAluguelDto itemAluguelDto;
        public ItemAluguelDtoCreateResult dtoCreateResult;
        public ItemAluguelDtoGetResult dtoGetResult;
        public ItemAluguelDtoUpdate dtoUpdate;
        public ItemAluguelDtoUpdateResult dtoUpdateResult;

        public ItemAluguelTeste()
        {
            ItemAluguelId = Guid.NewGuid();
            AluguelId = Guid.NewGuid();
            FilmeId = Guid.NewGuid();
            AluguelIdAlterado = Guid.NewGuid();
            FilmeIdAlterado = Guid.NewGuid();
            FilmeOriginal = new FilmeDtoLocacaoResult
                {
                    Id = FilmeId,
                    Titulo = Faker.Country.Name(),
                    Categoria = (Categoria) new Random()
                                .Next(Enum.GetNames(typeof(Categoria)).Length)
                };

            listItem = GetItens(new Random().Next(2, 5));

            itemAluguelDto = new ItemAluguelDto
            {
                AluguelId = AluguelId,
                FilmeId = FilmeId
            };

            dtoCreateResult = new ItemAluguelDtoCreateResult
            {
                Id = ItemAluguelId,
                Filme = FilmeOriginal,
                AluguelId = AluguelId,
                CreateAt = DateTime.UtcNow
            };

            dtoGetResult = new ItemAluguelDtoGetResult
            {
                Id = ItemAluguelId,
                Filme = FilmeOriginal,                
            };

            dtoUpdate = new ItemAluguelDtoUpdate
            {
                Id = ItemAluguelId,
                AluguelId = AluguelIdAlterado,
                FilmeId = Guid.NewGuid(),                
            };

            dtoUpdateResult = new ItemAluguelDtoUpdateResult
            {
                Id = ItemAluguelId,
                UpdateAt = DateTime.UtcNow,
                Filme = new FilmeDtoLocacaoResult
                {
                    Id = dtoUpdate.FilmeId,
                    Titulo = Faker.Country.Name(),
                    Categoria = (Categoria) new Random()
                                .Next(Enum.GetNames(typeof(Categoria)).Length)
                }
            };            
        }

        private List<ItemAluguelDtoGetResult> GetItens(int v)
        {
            List<ItemAluguelDtoGetResult> list = new List<ItemAluguelDtoGetResult>();
            for (int i = 0; i < v; i++)
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