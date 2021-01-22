using System;
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

namespace src.Api.Application.Test.Filme.QuandoRequisitarPut
{
    public class Retorno_Ok
    {
        private FilmesController _controller;
        private Mock<IFilmeService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Put()
        {
            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.PutAsync(It.IsAny<FilmeDtoUpdate>()))
                        .ReturnsAsync(                        
                            new FilmeDtoUpdateResult
                            {
                                Id = Guid.NewGuid(),
                                Titulo = Faker.Country.Name(),
                                Categoria = (Categoria) new Random()
                                            .Next(Enum.GetNames(typeof(Categoria)).Length),                                
                                CreateAt = DateTime.UtcNow,                                
                                UpdateAt = DateTime.UtcNow.AddHours(4.0)
                            }
                        );
            _controller = new FilmesController(_serviceMock.Object);           

            var response = await _controller.Put(It.IsAny<FilmeDtoUpdate>());
            Assert.True(response is OkObjectResult);            
        } 
    }
}