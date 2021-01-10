using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Usuario
{
    public class QuandoExecutarGetAll : UsuarioTeste
    {
        private IUsuarioService _service;
        private Mock<IUsuarioService> _serviceMock;

       [Fact]
       public async Task É_Possivel_Executar_GetAll()
       {
           _serviceMock = new Mock<IUsuarioService>();
           _serviceMock.Setup(m => m.GetAllAsync())
                       .ReturnsAsync(usuarioDtoGetResultCollection);
           _service = _serviceMock.Object;

           var result = await _service.GetAllAsync();
           Assert.NotNull(result);
           Assert.True(result.Count() > 0);           

           _serviceMock = new Mock<IUsuarioService>();
           _serviceMock.Setup(m => m.GetAllAsync())
                       .Returns(Task.FromResult((IEnumerable<UsuarioDtoGetResult>)null));
           _service = _serviceMock.Object;

           var nullObjectResult = await _service.GetAllAsync();
           Assert.Null(nullObjectResult);
       }        
    }
}