using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Usuario.QuandoRequisitarPost
{
    public class Retorno_BadRequest
    {
        private UsuariosController _controller;
        private Mock<IUsuarioService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Post()
        {
            _serviceMock = new Mock<IUsuarioService>();
            _serviceMock.Setup(m => m.PostAsync(It.IsAny<UsuarioDto>()))
                        .ReturnsAsync(                        
                            new UsuarioDtoCreateResult
                            {
                                Id = Guid.NewGuid(),
                                Nome = Faker.Name.FullName(),
                                Email = Faker.Internet.Email(),
                                CreateAt = DateTime.UtcNow,
                                TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,                                
                            }
                        );
            _controller = new UsuariosController(_serviceMock.Object);
            _controller.ModelState.AddModelError("ID", "Formato Inválido");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>()))
               .Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var response = await _controller.GetById(It.IsAny<Guid>());
            Assert.True(response is BadRequestResult);

            _serviceMock = new Mock<IUsuarioService>();
            _serviceMock.Setup(m => m.PostAsync(It.IsAny<UsuarioDto>()))
                        .Returns(Task.FromResult<UsuarioDtoCreateResult>(null));

            _controller = new UsuariosController(_serviceMock.Object);

            response = await _controller.Post(It.IsAny<UsuarioDto>());
            Assert.True(response is BadRequestResult);
        } 
    }
}