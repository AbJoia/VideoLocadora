using System;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Entities;
using src.Api.Domain.Model;
using Xunit;

namespace src.Api.Service.Test.AutoMapper
{
    public class FilmeMapper : BaseTesteService
    {
        [Fact]
        public void E_Possivel_Mapear_Modelos_Filme()
        {
            var model = new FilmeModel
            {
                Id = Guid.NewGuid(),
                Titulo = Faker.Country.Name(),
                Categoria = Domain.Enuns.Categoria.ROMANCE,
                CreateAt = DateTime.UtcNow,
                QtdLocacao = 0,
                UpdateAt = DateTime.UtcNow.AddHours(3.0),
                FuncionarioId = Guid.NewGuid()
            };

            var filmeDto = new FilmeDto
            {
                Titulo = Faker.Country.Name(),
                Categoria = Domain.Enuns.Categoria.BIOGRAFIA,
                FuncionarioId = Guid.NewGuid()                
            };

            var filmeDtoUpdate = new FilmeDtoUpdate
            {
                Id = Guid.NewGuid(),
                Titulo = Faker.Country.Name(),
                Categoria = Domain.Enuns.Categoria.BIOGRAFIA,
                FuncionarioId = Guid.NewGuid() 
            };

            //Model => Entity
            var entity = Mapper.Map<FilmeEntity>(model);
            Assert.NotNull(entity);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Titulo, model.Titulo);
            Assert.Equal(entity.Categoria, model.Categoria);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.QtdLocacao, model.QtdLocacao);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);            
            Assert.Equal(entity.FuncionarioId, model.FuncionarioId);

            //Entity => Dtos
            var dtoCreateResult = Mapper.Map<FilmeDtoCreateResult>(entity);
            Assert.NotNull(dtoCreateResult);
            Assert.Equal(dtoCreateResult.Id, entity.Id);
            Assert.Equal(dtoCreateResult.Titulo, entity.Titulo);
            Assert.Equal(dtoCreateResult.Categoria, entity.Categoria);
            Assert.Equal(dtoCreateResult.CreateAt, entity.CreateAt);

            var dtoGetResult = Mapper.Map<FilmeDtoGetResult>(entity);
            Assert.NotNull(dtoGetResult);
            Assert.Equal(dtoGetResult.Id, entity.Id);
            Assert.Equal(dtoGetResult.Titulo, entity.Titulo);
            Assert.Equal(dtoGetResult.Categoria, entity.Categoria);
            Assert.Equal(dtoGetResult.CreateAt, entity.CreateAt);
            Assert.Equal(dtoGetResult.QtdLocacao, entity.QtdLocacao);
            Assert.Equal(dtoGetResult.UpdateAt, entity.UpdateAt);

            var dtoUpdateResult = Mapper.Map<FilmeDtoUpdateResult>(entity);
            Assert.NotNull(dtoUpdateResult);
            Assert.Equal(dtoUpdateResult.Id, entity.Id);
            Assert.Equal(dtoUpdateResult.Titulo, entity.Titulo);
            Assert.Equal(dtoUpdateResult.Categoria, entity.Categoria);
            Assert.Equal(dtoUpdateResult.CreateAt, entity.CreateAt);            
            Assert.Equal(dtoUpdateResult.UpdateAt, entity.UpdateAt);

            //Dto => Model
            model = Mapper.Map<FilmeModel>(filmeDto);
            Assert.NotNull(entity);            
            Assert.Equal(filmeDto.Titulo, model.Titulo);
            Assert.Equal(filmeDto.Categoria, model.Categoria);                      
            Assert.Equal(filmeDto.FuncionarioId, model.FuncionarioId);

            model = Mapper.Map<FilmeModel>(filmeDtoUpdate);
            Assert.NotNull(entity);
            Assert.Equal(filmeDtoUpdate.Id, model.Id);            
            Assert.Equal(filmeDtoUpdate.Titulo, model.Titulo);
            Assert.Equal(filmeDtoUpdate.Categoria, model.Categoria);                      
            Assert.Equal(filmeDtoUpdate.FuncionarioId, model.FuncionarioId);           
                    
        }        
    }
}