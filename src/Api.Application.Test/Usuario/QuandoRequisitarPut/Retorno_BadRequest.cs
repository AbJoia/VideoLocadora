using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Usuario.QuandoRequisitarPut
{
    public class Retorno_BadRequest
    {
        private UsuariosController _controller;
        private Mock<IUsuarioService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Put()
        {
            _serviceMock = new Mock<IUsuarioService>();
            _serviceMock.Setup(m => m.PutAsync(It.IsAny<UsuarioDtoUpdate>()))
                        .ReturnsAsync(                        
                            new UsuarioDtoUpdateResult
                            {
                                Id = Guid.NewGuid(),
                                Nome = Faker.Name.FullName(),
                                Email = Faker.Internet.Email(),
                                CreateAt = DateTime.UtcNow,
                                TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                                UpdateAt = DateTime.UtcNow                                
                            }
                        );
            _controller = new UsuariosController(_serviceMock.Object);
            _controller.ModelState.AddModelError("ID", "Formato Inválido");

            var response = await _controller.Put(It.IsAny<UsuarioDtoUpdate>());
            Assert.True(response is BadRequestResult);

            _serviceMock = new Mock<IUsuarioService>();
            _serviceMock.Setup(m => m.PutAsync(It.IsAny<UsuarioDtoUpdate>()))
                        .Returns(Task.FromResult<UsuarioDtoUpdateResult>(null));

            _controller = new UsuariosController(_serviceMock.Object);

            response = await _controller.Put(It.IsAny<UsuarioDtoUpdate>());
            Assert.True(response is BadRequestResult);
        } 
    }
}