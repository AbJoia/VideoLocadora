using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.ItemAluguel;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.ItemAluguel.QuandoRequisitarPut
{
    public class Retorno_BadRequest
    {
        private ItensAlugueisController _controller;
        private Mock<IItemAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Put()
        {
            _serviceMock = new Mock<IItemAluguelService>();
            _serviceMock.Setup(m => m.PutItemAluguelAsync(It.IsAny<ItemAluguelDtoUpdate>()))
                        .ReturnsAsync(                        
                            new ItemAluguelDtoUpdateResult
                            {
                                Id = Guid.NewGuid(),
                                UpdateAt = DateTime.UtcNow,                                
                                Filme = new FilmeDtoLocacaoResult
                                {
                                    Id = Guid.NewGuid(),
                                    Titulo = Faker.Country.Name(),
                                    Categoria = Domain.Enuns.Categoria.BIOGRAFIA,
                                }                                                               
                            }
                        );
            _controller = new ItensAlugueisController(_serviceMock.Object);
            _controller.ModelState.AddModelError("ID", "Formato Inválido");

            var response = await _controller.Put(It.IsAny<ItemAluguelDtoUpdate>());
            Assert.True(response is BadRequestResult);

            _serviceMock = new Mock<IItemAluguelService>();
            _serviceMock.Setup(m => m.PutItemAluguelAsync(It.IsAny<ItemAluguelDtoUpdate>()))
                        .Returns(Task.FromResult<ItemAluguelDtoUpdateResult>(null));

            _controller = new ItensAlugueisController(_serviceMock.Object);

            response = await _controller.Put(It.IsAny<ItemAluguelDtoUpdate>());
            Assert.True(response is BadRequestResult);
        } 
    }
}