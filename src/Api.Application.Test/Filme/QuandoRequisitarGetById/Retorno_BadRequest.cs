using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Enuns;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Filme.QuandoRequisitarGetById
{
    public class Retorno_BadRequest
    {
        private FilmesController _controller;
        private Mock<IFilmeService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_GetById()
        {
            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.GetAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(                        
                            new FilmeDtoGetResult
                            {
                                Id = Guid.NewGuid(),
                                Titulo = Faker.Country.Name(),
                                Categoria = (Categoria) new Random()
                                            .Next(Enum.GetNames(typeof(Categoria)).Length),                                
                                CreateAt = DateTime.UtcNow,
                                QtdLocacao = new Random().Next(1, 100),
                                UpdateAt = DateTime.UtcNow.AddHours(4.0)
                            }
                        );
            _controller = new FilmesController(_serviceMock.Object);
            _controller.ModelState.AddModelError("ID", "Formato Inválido");

            var response = await _controller.GetById(It.IsAny<Guid>());
            Assert.True(response is BadRequestResult);
        } 
    }
}