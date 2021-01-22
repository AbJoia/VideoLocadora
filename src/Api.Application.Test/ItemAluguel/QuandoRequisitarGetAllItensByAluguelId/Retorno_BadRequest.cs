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
using src.Api.Domain.Enuns;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.ItemAluguel.QuandoRequisitarGetAllByAluguelId
{
    public class Retorno_BadRequest
    {
        private ItensAlugueisController _controller;
        private Mock<IItemAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
        {
            _serviceMock = new Mock<IItemAluguelService>();
            _serviceMock.Setup(m => m.GetAllItensByAluguelIdAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(new List<ItemAluguelDtoGetResult>()
                        {
                            new ItemAluguelDtoGetResult
                            {
                                Id = Guid.NewGuid(),
                                Filme = new FilmeDtoLocacaoResult
                                {
                                    Id = Guid.NewGuid(),
                                    Titulo = Faker.Name.FullName(),
                                    Categoria = (Categoria) new Random()
                                                .Next(Enum.GetNames(typeof(Categoria)).Length)
                                }
                            },
                            new ItemAluguelDtoGetResult
                            {
                                Id = Guid.NewGuid(),
                                Filme = new FilmeDtoLocacaoResult
                                {
                                    Id = Guid.NewGuid(),
                                    Titulo = Faker.Name.FullName(),
                                    Categoria = (Categoria) new Random()
                                                .Next(Enum.GetNames(typeof(Categoria)).Length)
                                }
                            }
                        });
            _controller = new ItensAlugueisController(_serviceMock.Object);
            _controller.ModelState.AddModelError("ID", "Formato Inválido");

            var response = await _controller.GetAllItensByAluguelId(It.IsAny<Guid>());
            Assert.True(response is BadRequestResult);
        }        
    }
}