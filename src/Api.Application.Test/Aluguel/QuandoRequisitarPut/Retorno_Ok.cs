using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Aluguel.QuandoRequisitarPut
{
    public class Retorno_Ok
    {
        private AlugueisController _controller;
        private Mock<IAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Put()
        {
            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.PutAluguelAsync(It.IsAny<AluguelDtoUpdate>()))
                        .ReturnsAsync(                        
                            new AluguelDtoUpdateResult
                            {
                                Id = Guid.NewGuid(),                                
                                UpdateAt = DateTime.UtcNow,
                                DataDevolucao = DateTime.UtcNow.AddHours(72.0),
                                UsuarioId = Guid.NewGuid()                                                             
                            }
                        );
            _controller = new AlugueisController(_serviceMock.Object);           

            var response = await _controller.Put(It.IsAny<AluguelDtoUpdate>());
            Assert.True(response is OkObjectResult);            
        }  
    }
}