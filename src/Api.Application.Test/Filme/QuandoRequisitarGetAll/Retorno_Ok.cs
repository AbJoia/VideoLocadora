using System;
using System.Collections.Generic;
using System.Linq;
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

namespace src.Api.Application.Test.Filme.QuandoRequisitarGetAll
{
    public class Retorno_Ok
    {
        private FilmesController _controller;
        private Mock<IFilmeService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
        {
            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.GetAllAsync())
                        .ReturnsAsync(new List<FilmeDtoGetResult>()
                        {
                            new FilmeDtoGetResult
                            {
                                Id = Guid.NewGuid(),
                                Titulo = Faker.Country.Name(),
                                Categoria = (Categoria) new Random()
                                            .Next(Enum.GetNames(typeof(Categoria)).Length),                                
                                CreateAt = DateTime.UtcNow,
                                QtdLocacao = new Random().Next(1, 100),
                                UpdateAt = DateTime.UtcNow.AddHours(4.0)
                            },
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

                        });
            _controller = new FilmesController(_serviceMock.Object);            

            var response = await _controller.GetAll();
            Assert.True(response is OkObjectResult);
            var obj = ((OkObjectResult)response).Value as List<FilmeDtoGetResult>;
            Assert.True(obj.Count() == 2);
        } 
    }
}