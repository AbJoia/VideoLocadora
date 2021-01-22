using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Funcionario.QuandoRequisitarPut
{
    public class Retorno_Ok
    {
        private FuncionariosController _controller;
        private Mock<IFuncionarioService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Put()
        {
            _serviceMock = new Mock<IFuncionarioService>();
            _serviceMock.Setup(m => m.PutAsync(It.IsAny<FuncionarioDtoUpdate>()))
                        .ReturnsAsync(                        
                            new FuncionarioDtoUpdateResult
                            {
                                Id = Guid.NewGuid(),
                                Nome = Faker.Name.FullName(),
                                Email = Faker.Internet.Email(),
                                CreateAt = DateTime.UtcNow,
                                TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                                UpdateAt = DateTime.UtcNow,
                                Matricula = 8888,                                
                            }
                        );
            _controller = new FuncionariosController(_serviceMock.Object);           

            var response = await _controller.Put(It.IsAny<FuncionarioDtoUpdate>());
            Assert.True(response is OkObjectResult);            
        } 
    }
}