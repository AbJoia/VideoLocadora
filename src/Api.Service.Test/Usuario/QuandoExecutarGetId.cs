using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Usuario
{
    public class QuandoExecutarGetId : UsuarioTeste
    {
        private IUsuarioService _service;
        private Mock<IUsuarioService> _serviceMock;

       [Fact]
       public async Task É_Possivel_Executar_GetId()
       {
           _serviceMock = new Mock<IUsuarioService>();
           _serviceMock.Setup(m => m.GetAsync(It.IsAny<Guid>()))
                       .ReturnsAsync(usuarioDtoGetResult);
           _service = _serviceMock.Object;

           var result = await _service.GetAsync(IdUsuario);
           Assert.NotNull(result);
           Assert.Equal(result.Id, IdUsuario);
           Assert.Equal(result.Nome, NomeUsuario);
           Assert.Equal(result.Email, EmailUsuario);
           Assert.Equal(result.CreateAt, CreateAtUsuario);
           Assert.True(result.TipoUsuario == Domain.Enuns.TipoUsuario.CLIENTE);

           _serviceMock = new Mock<IUsuarioService>();
           _serviceMock.Setup(m => m.GetAsync(IdUsuario))
                       .ReturnsAsync(usuarioDtoGetResult);
           _service = _serviceMock.Object;

           var nullResult = await _service.GetAsync(Guid.NewGuid());
           Assert.Null(nullResult);

           _serviceMock = new Mock<IUsuarioService>();
           _serviceMock.Setup(m => m.GetAsync(It.IsAny<Guid>()))
                       .Returns(Task.FromResult((UsuarioDtoGetResult)null));
           _service = _serviceMock.Object;

           var nullObjectResult = await _service.GetAsync(Guid.NewGuid());
           Assert.Null(nullObjectResult);
       }        
    }
}