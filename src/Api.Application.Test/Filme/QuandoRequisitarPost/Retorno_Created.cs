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

namespace src.Api.Application.Test.Filme.QuandoRequisitarPost
{
    public class Retorno_Created
    {
        private FilmesController _controller;
        private Mock<IFilmeService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Post()
        {
            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.PostAsync(It.IsAny<FilmeDto>()))
                        .ReturnsAsync(                        
                            new FilmeDtoCreateResult
                            {
                                Id = Guid.NewGuid(),
                                Titulo = Faker.Country.Name(),
                                Categoria = (Categoria) new Random()
                                            .Next(Enum.GetNames(typeof(Categoria)).Length),                                
                                CreateAt = DateTime.UtcNow                                                           
                            }
                        );
            _controller = new FilmesController(_serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>()))
               .Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var response = await _controller.Post(It.IsAny<FilmeDto>());
            Assert.True(response is CreatedResult);            
        }
    }
}