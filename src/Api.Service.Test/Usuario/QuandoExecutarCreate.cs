using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Usuario
{
    public class QuandoExecutarCreate : UsuarioTeste
    {
        private IUsuarioService _service;
        private Mock<IUsuarioService> _serviceMock;
        [Fact]
        public async Task É_Possivel_Executar_Create()
        {
            _serviceMock = new Mock<IUsuarioService>();
            _serviceMock.Setup(m => m.PostAsync(usuarioDto))
                        .ReturnsAsync(usuarioDtoCreateResult);
            _service = _serviceMock.Object;

            var result = await _service.PostAsync(usuarioDto);
            Assert.NotNull(result);
            Assert.Equal(result.Nome, usuarioDto.Nome);
            Assert.Equal(result.Email, usuarioDto.Email);           
        }
    }
}