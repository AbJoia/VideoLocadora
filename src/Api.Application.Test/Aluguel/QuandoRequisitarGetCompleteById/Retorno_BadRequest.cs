using System;
using System.Collections.Generic;
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
    public class Retorno_BadRequest
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
                                
                                Usuario = new UsuarioDtoGetResult
                                {
                                    Id = Guid.NewGuid(),
                                    Email = Faker.Internet.Email(),
                                    Nome = Faker.Name.FullName(),
                                    TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                                    CreateAt = DateTime.UtcNow,
                                    UpdateAt = DateTime.UtcNow.AddHours(2.0),
                                },
                                DataDevolucao = DateTime.UtcNow.AddHours(72.0)
                            });
            
            _controller = new AlugueisController(_serviceMock.Object);
            _controller.ModelState.AddModelError("ID", "Formato Inválido");

            var response = await _controller.GetAllByUsuarioId(It.IsAny<Guid>());
            Assert.True(response is BadRequestResult);                     
        }
    }
}