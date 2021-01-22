using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Filme.QuandoRequisitarDelete
{
    public class Retorno_NotFound
    {
        private FilmesController _controller;
        private Mock<IFilmeService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Delete()
        {
            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(false);
            _controller = new FilmesController(_serviceMock.Object);                     

            var response = await _controller.Delete(It.IsAny<Guid>());
            Assert.True(response is NotFoundResult);            
        }
    }
}