using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Aluguel
{
    public class QuandoExecutarCreate : AluguelTeste
    {
        private IAluguelService _service;
        private Mock<IAluguelService> _serviceMock;        

        [Fact]
        public async Task E_Possivel_Executar_Create()
        {
            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.PostAluguelAsync(aluguelDto))
                        .ReturnsAsync(aluguelDtoCreateResult);
            _service = _serviceMock.Object;

            var result = await _service.PostAluguelAsync(aluguelDto);
            Assert.NotNull(result);
            Assert.Equal(result.Id, AluguelId);
            Assert.Equal(result.DataDevolucao, DataDevolucao);
            Assert.NotNull(result.Usuario);
            Assert.IsType<UsuarioDtoGetResult>(result.Usuario);

            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.PostAluguelAsync(It.IsAny<AluguelDto>()))
                        .Returns(Task.FromResult<AluguelDtoCreateResult>(null));
            _service = _serviceMock.Object;

            result = await _service.PostAluguelAsync(aluguelDto);
            Assert.Null(result);
        }
    }
}