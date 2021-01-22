using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Api.Application.Controllers;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Application.Test.Aluguel.QuandoRequisitarPost
{
    public class Retorno_Created
    {
        private AlugueisController _controller;
        private Mock<IAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Invocar_a_Controller_Post()
        {
            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.PostAluguelAsync(It.IsAny<AluguelDto>()))
                        .ReturnsAsync(                        
                            new AluguelDtoCreateResult
                            {
                                Id = Guid.NewGuid(),                                
                                CreateAt = DateTime.UtcNow,
                                DataDevolucao = DateTime.UtcNow.AddHours(72.0),
                                Usuario = new UsuarioDtoGetResult
                                {
                                    Id = Guid.NewGuid(),
                                    Email = Faker.Internet.Email(),
                                    Nome = Faker.Name.FullName(),
                                    CreateAt = DateTime.UtcNow,
                                    TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                                    UpdateAt = DateTime.UtcNow.AddHours(2.0),  
                                }                                                               
                            }
                        );
            _controller = new AlugueisController(_serviceMock.Object);            

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>()))
               .Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var response = await _controller.Post(It.IsAny<AluguelDto>());
            Assert.True(response is CreatedResult);            
        } 
    }
}