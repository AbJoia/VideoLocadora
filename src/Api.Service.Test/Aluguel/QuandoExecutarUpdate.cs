using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Aluguel
{
    public class QuandoExecutarUpdate : AluguelTeste
    {
        private IAluguelService _service;
        private Mock<IAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_Update()
        {
            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.PutAluguelAsync(aluguelDtoUpdate))
                        .ReturnsAsync(aluguelDtoUpdateResult);
            _service = _serviceMock.Object;

            var result = await _service.PutAluguelAsync(aluguelDtoUpdate);
            Assert.NotNull(result);
            Assert.Equal(result.Id, AluguelId);
            Assert.NotEqual(result.DataDevolucao, DataDevolucao);
            Assert.NotEqual(result.UsuarioId, UsuarioId);

            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.PutAluguelAsync(It.IsAny<AluguelDtoUpdate>()))
                        .Returns(Task.FromResult<AluguelDtoUpdateResult>(null));
            _service = _serviceMock.Object;

            result = await _service.PutAluguelAsync(aluguelDtoUpdate);
            Assert.Null(result);
        }        
    }
}