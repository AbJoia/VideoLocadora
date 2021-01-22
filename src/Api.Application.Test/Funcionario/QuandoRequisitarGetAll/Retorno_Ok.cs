using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Funcionario.QuandoRequisitarGetAll
{
    public class Retorno_Ok
    {
        private FuncionariosController _controller;
        private Mock<IFuncionarioService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
        {
            _serviceMock = new Mock<IFuncionarioService>();
            _serviceMock.Setup(m => m.GetAllAsync())
                        .ReturnsAsync(new List<FuncionarioDtoGetResult>()
                        {
                            new FuncionarioDtoGetResult
                            {
                                Id = Guid.NewGuid(),
                                Nome = Faker.Name.FullName(),
                                Email = Faker.Internet.Email(),
                                CreateAt = DateTime.UtcNow,
                                Matricula = 5589
                            },
                            new FuncionarioDtoGetResult
                            {
                                Id = Guid.NewGuid(),
                                Nome = Faker.Name.FullName(),
                                Email = Faker.Internet.Email(),
                                CreateAt = DateTime.UtcNow,
                                Matricula = 1289
                            }
                        });
            _controller = new FuncionariosController(_serviceMock.Object);            

            var response = await _controller.GetAll();
            Assert.True(response is OkObjectResult);
            var obj = ((OkObjectResult) response).Value as List<FuncionarioDtoGetResult>;
            Assert.True(obj.Count() == 2);
        } 
    }
}