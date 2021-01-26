using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.ItemAluguel;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Enuns;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Aluguel.QuandoRequisitarGetCompleteById
{
    public class Retorno_Ok
    {
        private AlugueisController _controller;
        private Mock<IAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_GetCompleteById()
        {
            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.GetCompleteByIdAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(
                            new AluguelDtoCompleteResult
                            {
                                Id = Guid.NewGuid(),
                                ItensAluguel = new List<ItemAluguelDtoGetResult>()
                                {
                                    new ItemAluguelDtoGetResult
                                    {
                                        Id = Guid.NewGuid(),
                                        Filme = new FilmeDtoLocacaoResult
                                        {
                                            Id = Guid.NewGuid(),
                                            Titulo = Faker.Country.Name(),
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
                                            Titulo = Faker.Country.Name(),
                                            Categoria = (Categoria) new Random()
                                                        .Next(Enum.GetNames(typeof(Categoria)).Length)
                                        }    
                                    }    
                                },
                                
                                Usuario = new UsuarioDtoLocacaoResult
                                {
                                    Id = Guid.NewGuid(),
                                    Email = Faker.Internet.Email(),
                                    Nome = Faker.Name.FullName(),                                    
                                },
                                DataDevolucao = DateTime.UtcNow.AddHours(72.0)
                            });
            
            _controller = new AlugueisController(_serviceMock.Object);            

            var response = await _controller.GetCompleteById(It.IsAny<Guid>());
            Assert.True(response is OkObjectResult);

            var obj = ((OkObjectResult)response).Value as AluguelDtoCompleteResult;
            Assert.NotNull(obj);
            Assert.True(obj.ItensAluguel.Count() == 2);                     
        }
    }
}