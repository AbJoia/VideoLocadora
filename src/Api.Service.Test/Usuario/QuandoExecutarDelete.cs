using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Usuario
{
    public class QuandoExecutarDelete : UsuarioTeste
    {
        private IUsuarioService _service;
        private Mock<IUsuarioService> _serviceMock;

        [Fact]
        public async Task É_Possivel_Executar_Delete()
        {
            _serviceMock = new Mock<IUsuarioService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.DeleteAsync(IdUsuario);
            Assert.True(result);

            _serviceMock = new Mock<IUsuarioService>();
            _serviceMock.Setup(m => m.DeleteAsync(IdUsuario))
                        .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var nullResult = await _service.DeleteAsync(Guid.NewGuid());
            Assert.False(nullResult);

            _serviceMock = new Mock<IUsuarioService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(false);
            _service = _serviceMock.Object;

            var falseResult = await _service.DeleteAsync(Guid.NewGuid());
            Assert.False(falseResult);
        }        
    }
}