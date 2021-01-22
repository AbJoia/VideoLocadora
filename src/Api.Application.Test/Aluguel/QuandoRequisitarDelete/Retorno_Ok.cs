using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Aluguel.QuandoRequisitarDelete
{
    public class Retorno_Ok
    {
        private AlugueisController _controller;
        private Mock<IAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Delete()
        {
            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.DeleteAluguelAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(true);
            _controller = new AlugueisController(_serviceMock.Object);                     

            var response = await _controller.Delete(It.IsAny<Guid>());
            Assert.True(response is OkObjectResult);            
        }
    }
}