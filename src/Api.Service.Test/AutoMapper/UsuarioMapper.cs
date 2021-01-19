using System;
using System.Collections.Generic;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Entities;
using src.Api.Domain.Enuns;
using src.Api.Domain.Model;
using Xunit;

namespace src.Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTesteService
    {
        [Fact]
        public void E_Possivel_Mapear_Modelos()
        {
            var model = new UsuarioModel()
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                CreateAt = DateTime.UtcNow,                        
                UpdateAt = DateTime.UtcNow.AddHours(2.5) 
            };

            var usuarioDto = new UsuarioDto()
            {
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            var usuarioDtoUpdate = new UsuarioDtoUpdate()
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            //Model => Entity
            var entity = Mapper.Map<UsuarioEntity>(model);
            Assert.NotNull(entity);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Nome, model.Nome);
            Assert.Equal(entity.Email, model.Email);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.TipoUsuario, model.TipoUsuario);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            //Entity => Dtos
            var dtoCreateResult = Mapper.Map<UsuarioDtoCreateResult>(entity);
            Assert.NotNull(dtoCreateResult);
            Assert.Equal(dtoCreateResult.Id, entity.Id);
            Assert.Equal(dtoCreateResult.Nome, entity.Nome);
            Assert.Equal(dtoCreateResult.Email, entity.Email);
            Assert.Equal(dtoCreateResult.CreateAt, entity.CreateAt);
            Assert.Equal(dtoCreateResult.TipoUsuario, entity.TipoUsuario);

            var dtoUpdateResult = Mapper.Map<UsuarioDtoUpdateResult>(entity);
            Assert.NotNull(dtoUpdateResult);
            Assert.Equal(dtoUpdateResult.Id, entity.Id);
            Assert.Equal(dtoUpdateResult.Nome, entity.Nome);
            Assert.Equal(dtoUpdateResult.Email, entity.Email);
            Assert.Equal(dtoUpdateResult.CreateAt, entity.CreateAt);
            Assert.Equal(dtoUpdateResult.UpdateAt, entity.UpdateAt);
            Assert.Equal(dtoUpdateResult.TipoUsuario, entity.TipoUsuario); 

            var dtoLocacaoResult = Mapper.Map<UsuarioDtoLocacaoResult>(entity);
            Assert.NotNull(dtoLocacaoResult);
            Assert.Equal(dtoLocacaoResult.Id, entity.Id);
            Assert.Equal(dtoLocacaoResult.Nome, entity.Nome);
            Assert.Equal(dtoLocacaoResult.Email, entity.Email);

            var dtoGetResult = Mapper.Map<UsuarioDtoGetResult>(entity);
            Assert.NotNull(dtoGetResult);
            Assert.Equal(dtoGetResult.Id, entity.Id);
            Assert.Equal(dtoGetResult.Nome, entity.Nome);
            Assert.Equal(dtoGetResult.Email, entity.Email);
            Assert.Equal(dtoGetResult.CreateAt, entity.CreateAt);
            Assert.Equal(dtoGetResult.UpdateAt, entity.UpdateAt);
            Assert.Equal(dtoGetResult.TipoUsuario, entity.TipoUsuario); 

            //Dto => Model
            model = Mapper.Map<UsuarioModel>(usuarioDto);
            Assert.NotNull(model);            
            Assert.Equal(model.Nome, usuarioDto.Nome);
            Assert.Equal(model.Email, usuarioDto.Email);

            model = Mapper.Map<UsuarioModel>(usuarioDtoUpdate);
            Assert.NotNull(model);   
            Assert.Equal(model.Id, usuarioDtoUpdate.Id);         
            Assert.Equal(model.Nome, usuarioDtoUpdate.Nome);
            Assert.Equal(model.Email, usuarioDtoUpdate.Email);
        }          
    }
}