using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Aluguel.QuandoRequisitarPost
{
    public class Retorno_BadRequest
    {
        private FuncionariosController _controller;
        private Mock<IFuncionarioService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Post()
        {
            _serviceMock = new Mock<IFuncionarioService>();
            _serviceMock.Setup(m => m.PostAsync(It.IsAny<FuncionarioDto>()))
                        .ReturnsAsync(                        
                            new FuncionarioDtoCreateResult
                            {
                                Id = Guid.NewGuid(),
                                Nome = Faker.Name.FullName(),
                                Email = Faker.Internet.Email(),
                                CreateAt = DateTime.UtcNow,
                                TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                                Matricula = 1010                                
                            }
                        );
            _controller = new FuncionariosController(_serviceMock.Object);
            _controller.ModelState.AddModelError("ID", "Formato Inválido");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>()))
               .Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var response = await _controller.GetById(It.IsAny<Guid>());
            Assert.True(response is BadRequestResult);

            _serviceMock = new Mock<IFuncionarioService>();
            _serviceMock.Setup(m => m.PostAsync(It.IsAny<FuncionarioDto>()))
                        .Returns(Task.FromResult<FuncionarioDtoCreateResult>(null));

            _controller = new FuncionariosController(_serviceMock.Object);

            response = await _controller.Post(It.IsAny<FuncionarioDto>());
            Assert.True(response is BadRequestResult);
        } 
    }
}