using System;
using System.Collections.Generic;
using System.Linq;
using src.Api.Domain.Dtos.ItemAluguel;
using src.Api.Domain.Entities;
using src.Api.Domain.Enuns;
using src.Api.Domain.Model;
using Xunit;

namespace src.Api.Service.Test.AutoMapper
{
    public class ItemAluguelMapper : BaseTesteService
    {
        [Fact]
        public void E_Possivel_Mapear_Modelos_ItemAluguel()
        {
            var model = new ItemAluguelModel
            {
                Id = Guid.NewGuid(),
                AluguelId = Guid.NewGuid(),
                FilmeId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow.AddDays(1.0),
            };

            var listItens = new List<ItemAluguelEntity>();          
            var aluguelId = Guid.NewGuid();

            for(int i = 0; i < new Random().Next(2, 5); i++)
            {
                var filme = new FilmeEntity
                {
                    Id = Guid.NewGuid(),
                    Titulo = Faker.Country.Name(),
                    Categoria = (Categoria) new Random()
                                .Next(Enum.GetNames(typeof(Categoria)).Length)
                };

                listItens.Add(
                  new ItemAluguelEntity
                    {
                        Id = Guid.NewGuid(),
                        FilmeId = filme.Id,
                        Filme = filme,
                        AluguelId = aluguelId,
                        CreateAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow.AddDays(1.0),
                    }      
                );      
            }              
            

            //Model => Entity
            var entity = Mapper.Map<ItemAluguelEntity>(model);
            Assert.NotNull(entity);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.AluguelId, model.AluguelId);
            Assert.Equal(entity.FilmeId, model.FilmeId);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            //Entity => Dtos
            var dtoCreateResult = Mapper.Map<ItemAluguelDtoCreateResult>(listItens[0]);
            Assert.NotNull(dtoCreateResult);
            Assert.Equal(dtoCreateResult.Id, listItens[0].Id);
            Assert.Equal(dtoCreateResult.AluguelId, listItens[0].AluguelId);
            Assert.Equal(dtoCreateResult.CreateAt, listItens[0].CreateAt);
            Assert.Equal(dtoCreateResult.FilmeId, listItens[0].FilmeId);
            
            var dtoGetResult = Mapper.Map<ItemAluguelDtoGetResult>(listItens[0]);
            Assert.NotNull(dtoGetResult);
            Assert.Equal(dtoGetResult.Id, listItens[0].Id);            
            Assert.Equal(dtoGetResult.Filme.Id, listItens[0].Filme.Id);
            Assert.Equal(dtoGetResult.Filme.Titulo, listItens[0].Filme.Titulo);
            Assert.Equal(dtoGetResult.Filme.Categoria, listItens[0].Filme.Categoria);

            var listResult = Mapper.Map<IEnumerable<ItemAluguelDtoGetResult>>(listItens);
            Assert.NotNull(listItens);
            for(int i = 0; i < listResult.Count(); i++)
            {
                Assert.Equal(listResult.ToList()[i].Id, listItens[i].Id);            
                Assert.Equal(listResult.ToList()[i].Filme.Id, listItens[i].Filme.Id);
                Assert.Equal(listResult.ToList()[i].Filme.Titulo, listItens[i].Filme.Titulo);
                Assert.Equal(listResult.ToList()[i].Filme.Categoria, listItens[i].Filme.Categoria);
            }

            var dtoUpdateResult = Mapper.Map<ItemAluguelDtoUpdateResult>(listItens[0]);
            Assert.NotNull(dtoUpdateResult);
            Assert.Equal(dtoUpdateResult.Id, listItens[0].Id);            
            Assert.Equal(dtoUpdateResult.UpdateAt, listItens[0].UpdateAt);
            Assert.Equal(dtoUpdateResult.FilmeId, listItens[0].Filme.Id);            

            //Dto => Model
            var dtoCreate = new ItemAluguelDto
            {                
                AluguelId = Guid.NewGuid(),
                FilmeId = Guid.NewGuid(),
            };

            model = Mapper.Map<ItemAluguelModel>(dtoCreate);
            Assert.NotNull(model);            
            Assert.Equal(model.AluguelId, dtoCreate.AluguelId);
            Assert.Equal(model.FilmeId, dtoCreate.FilmeId);

            var dtoUpdate = new ItemAluguelDtoUpdate
            {
                Id = Guid.NewGuid(),                
                AluguelId = Guid.NewGuid(),
                FilmeId = Guid.NewGuid(),
            };

            model = Mapper.Map<ItemAluguelModel>(dtoUpdate);
            Assert.NotNull(model);            
            Assert.Equal(model.Id, dtoUpdate.Id);
            Assert.Equal(model.AluguelId, dtoUpdate.AluguelId);  
            Assert.Equal(model.FilmeId, dtoUpdate.FilmeId);                        
        }
    }
}