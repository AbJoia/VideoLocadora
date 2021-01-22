using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Usuario.QuandoRequisitarGetById
{
    public class Retorno_BadRequest
    {
        private UsuariosController _controller;
        private Mock<IUsuarioService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_GetById()
        {
            _serviceMock = new Mock<IUsuarioService>();
            _serviceMock.Setup(m => m.GetAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(                        
                            new UsuarioDtoGetResult
                            {
                                Id = Guid.NewGuid(),
                                Nome = Faker.Name.FullName(),
                                Email = Faker.Internet.Email(),
                                CreateAt = DateTime.UtcNow,
                                TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                                UpdateAt = DateTime.UtcNow.AddHours(1.5)
                            }
                        );
            _controller = new UsuariosController(_serviceMock.Object);
            _controller.ModelState.AddModelError("ID", "Formato Inválido");

            var response = await _controller.GetById(It.IsAny<Guid>());
            Assert.True(response is BadRequestResult);
        } 
    }
}