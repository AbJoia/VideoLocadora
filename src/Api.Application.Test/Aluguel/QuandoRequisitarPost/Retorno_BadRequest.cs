using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Funcionario.QuandoRequisitarPost
{
    public class Retorno_BadRequest
    {
        private AlugueisController _controller;
        private Mock<IAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Post()
        {
            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.PostAluguelAsync(It.IsAny<AluguelDto>()))
                        .ReturnsAsync(                        
                            new AluguelDtoCreateResult
                            {
                                Id = Guid.NewGuid(),                                
                                CreateAt = DateTime.UtcNow,
                                DataDevolucao = DateTime.UtcNow.AddHours(72.0),
                                UsuarioId = Guid.NewGuid()                                                              
                            }
                        );
            _controller = new AlugueisController(_serviceMock.Object);
            _controller.ModelState.AddModelError("ID", "Formato Inválido");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>()))
               .Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var response = await _controller.Post(It.IsAny<AluguelDto>());
            Assert.True(response is BadRequestResult);

            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.PostAluguelAsync(It.IsAny<AluguelDto>()))
                        .Returns(Task.FromResult<AluguelDtoCreateResult>(null));

            _controller = new AlugueisController(_serviceMock.Object);

            response = await _controller.Post(It.IsAny<AluguelDto>());
            Assert.True(response is BadRequestResult);
        } 
    }
}