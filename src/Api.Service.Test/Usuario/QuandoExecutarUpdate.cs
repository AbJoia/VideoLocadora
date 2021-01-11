using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Usuario
{
    public class QuandoExecutarUpdate : UsuarioTeste
    {
        private IUsuarioService _service;
        private Mock<IUsuarioService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_Update()
        {
           _serviceMock = new Mock<IUsuarioService>();
           _serviceMock.Setup(m => m.PutAsync(usuarioDtoUpdate))
                       .ReturnsAsync(usuarioDtoUpdateResult);
            _service = _serviceMock.Object;

            var result = await _service.PutAsync(usuarioDtoUpdate);
            Assert.NotNull(result);
            Assert.Equal(result.Nome, NomeAlterado);
            Assert.Equal(result.Email, EmailAlterado);
            Assert.True(result.TipoUsuario == Domain.Enuns.TipoUsuario.CLIENTE);
        }        
    }
}