using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Aluguel.QuandoRequisitarGetAllByUsuarioId
{
    public class Retorno_BadRequest
    {
        private AlugueisController _controller;
        private Mock<IAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_GetAllByUsuarioId()
        {
            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.GetAllByUsuarioIdAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(new List<AluguelDtoGetResult>()
                        {
                            new AluguelDtoGetResult
                            {
                                Id = Guid.NewGuid(),
                                Usuario = new UsuarioDtoLocacaoResult
                                {
                                    Id = Guid.NewGuid(),
                                    Email = Faker.Internet.Email(),
                                    Nome = Faker.Name.FullName(),                                    
                                },
                                DataDevolucao = DateTime.UtcNow.AddHours(72.0)
                            },
                            new AluguelDtoGetResult
                            {
                                Id = Guid.NewGuid(),
                                Usuario = new UsuarioDtoLocacaoResult
                                {
                                    Id = Guid.NewGuid(),
                                    Email = Faker.Internet.Email(),
                                    Nome = Faker.Name.FullName(),                                    
                                },
                                DataDevolucao = DateTime.UtcNow.AddHours(72.0)
                            }
                        });
            
            _controller = new AlugueisController(_serviceMock.Object);
            _controller.ModelState.AddModelError("ID", "Formato Inválido");

            var response = await _controller.GetAllByUsuarioId(It.IsAny<Guid>());
            Assert.True(response is BadRequestResult);                     
        }
    }
}