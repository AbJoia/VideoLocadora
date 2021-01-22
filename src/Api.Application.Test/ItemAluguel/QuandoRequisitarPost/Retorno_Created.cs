using System;
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

namespace src.Api.Application.Test.ItemAluguel.QuandoRequisitarPost
{
    public class Retorno_Created
    {
        private ItensAlugueisController _controller;
        private Mock<IItemAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Post()
        {
            _serviceMock = new Mock<IItemAluguelService>();
            _serviceMock.Setup(m => m.PostItemAluguelAsync(It.IsAny<ItemAluguelDto>()))
                        .ReturnsAsync(                        
                            new ItemAluguelDtoCreateResult
                            {
                                Id = Guid.NewGuid(), 
                                AluguelId = Guid.NewGuid(),                               
                                CreateAt = DateTime.UtcNow,
                                Filme = new FilmeDtoLocacaoResult
                                {
                                    Id = Guid.NewGuid(),
                                    Titulo = Faker.Country.Name(),
                                    Categoria = Domain.Enuns.Categoria.BIOGRAFIA,
                                }                                                               
                            }
                        );
            _controller = new ItensAlugueisController(_serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>()))
               .Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var response = await _controller.Post(It.IsAny<ItemAluguelDto>());
            Assert.True(response is CreatedResult);            
        }
    }
}