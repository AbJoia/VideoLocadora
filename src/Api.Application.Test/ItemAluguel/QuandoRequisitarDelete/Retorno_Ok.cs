using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.ItemAluguel.QuandoRequisitarDelete
{
    public class Retorno_Ok
    {
        private ItensAlugueisController _controller;
        private Mock<IItemAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Delete()
        {
            _serviceMock = new Mock<IItemAluguelService>();
            _serviceMock.Setup(m => m.DeleteItemAluguelAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(true);
            _controller = new ItensAlugueisController(_serviceMock.Object);                     

            var response = await _controller.Delete(It.IsAny<Guid>());
            Assert.True(response is OkObjectResult);            
        }
    }
}