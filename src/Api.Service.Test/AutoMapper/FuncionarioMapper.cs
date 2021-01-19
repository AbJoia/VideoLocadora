using System;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Entities;
using src.Api.Domain.Model;
using Xunit;

namespace src.Api.Service.Test.AutoMapper
{
    public class FuncionarioMapper : BaseTesteService
    {
        [Fact]
        public void E_Possivel_Mapear_Modelos_Funcionario()
        {
            var model = new FuncionarioModel
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Matricula = Faker.RandomNumber.Next(1000, 99999),
                CreateAt = DateTime.UtcNow,
                Senha = Faker.RandomNumber.Next(1000, 99999).ToString(),
                TipoUsuario = Domain.Enuns.TipoUsuario.FUNCIONARIO,
                UpdateAt = DateTime.UtcNow.AddHours(3.0)
            };

            var funcionarioDto = new FuncionarioDto
            {
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Senha = Faker.RandomNumber.Next(1000, 9999).ToString()
            };

            var funcionarioDtoUpdate = new FuncionarioDtoUpdate
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Senha = Faker.RandomNumber.Next(1000, 9999).ToString()
            };

            //Model => Entity
            var entity = Mapper.Map<FuncionarioEntity>(model);
            Assert.NotNull(entity);
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Nome, entity.Nome);
            Assert.Equal(model.Email, entity.Email);
            Assert.Equal(model.Matricula, entity.Matricula);
            Assert.Equal(model.Senha, entity.Senha);
            Assert.Equal(model.TipoUsuario, entity.TipoUsuario);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);

            //Entity => Dtos
            var dtoCreateResult = Mapper.Map<FuncionarioDtoCreateResult>(entity);
            Assert.NotNull(dtoCreateResult);
            Assert.Equal(dtoCreateResult.Id, entity.Id);
            Assert.Equal(dtoCreateResult.Nome, entity.Nome);
            Assert.Equal(dtoCreateResult.Email, entity.Email);
            Assert.Equal(dtoCreateResult.Matricula, entity.Matricula);            
            Assert.Equal(dtoCreateResult.TipoUsuario, entity.TipoUsuario);
            Assert.Equal(dtoCreateResult.CreateAt, entity.CreateAt);

            var dtoGetResult = Mapper.Map<FuncionarioDtoGetResult>(entity);
            Assert.NotNull(dtoGetResult);
            Assert.Equal(dtoGetResult.Id, entity.Id);
            Assert.Equal(dtoGetResult.Nome, entity.Nome);
            Assert.Equal(dtoGetResult.Email, entity.Email);
            Assert.Equal(dtoGetResult.Matricula, entity.Matricula);          
            Assert.Equal(dtoGetResult.CreateAt, entity.CreateAt);

            var dtoUpdateResult = Mapper.Map<FuncionarioDtoUpdateResult>(entity);
            Assert.NotNull(dtoUpdateResult);
            Assert.Equal(dtoUpdateResult.Id, entity.Id);
            Assert.Equal(dtoUpdateResult.Nome, entity.Nome);
            Assert.Equal(dtoUpdateResult.Email, entity.Email);
            Assert.Equal(dtoUpdateResult.Matricula, entity.Matricula);            
            Assert.Equal(dtoUpdateResult.TipoUsuario, entity.TipoUsuario);
            Assert.Equal(dtoUpdateResult.CreateAt, entity.CreateAt);
            Assert.Equal(dtoUpdateResult.UpdateAt, entity.UpdateAt);

            //Dto => Model
            model = Mapper.Map<FuncionarioModel>(funcionarioDto);
            Assert.NotNull(model);            
            Assert.Equal(model.Nome, funcionarioDto.Nome);
            Assert.Equal(model.Email, funcionarioDto.Email);            
            Assert.Equal(model.Senha, funcionarioDto.Senha);

            model = Mapper.Map<FuncionarioModel>(funcionarioDtoUpdate);
            Assert.NotNull(model); 
            Assert.Equal(model.Id, funcionarioDtoUpdate.Id);           
            Assert.Equal(model.Nome, funcionarioDtoUpdate.Nome);
            Assert.Equal(model.Email, funcionarioDtoUpdate.Email);            
            Assert.Equal(model.Senha, funcionarioDtoUpdate.Senha);                    

        }
    }
}