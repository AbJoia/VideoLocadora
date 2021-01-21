using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Aluguel
{
    public class QuandoExecutarDelete : AluguelTeste
    {
        private IAluguelService _service;
        private Mock<IAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_Delete()
        {
            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.DeleteAluguelAsync(AluguelId))
                        .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.DeleteAluguelAsync(AluguelId);            
            Assert.True(result);

             _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.DeleteAluguelAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(false);
            _service = _serviceMock.Object;

            result = await _service.DeleteAluguelAsync(Guid.NewGuid());            
            Assert.False(result);            
        }
    }
}