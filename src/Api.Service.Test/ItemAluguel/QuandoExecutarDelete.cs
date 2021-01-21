using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.ItemAluguel
{
    public class QuandoExecutarDelete : ItemAluguelTeste
    {
        private IItemAluguelService _service;
        private Mock<IItemAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_Delete()
        {
            _serviceMock = new Mock<IItemAluguelService>();
            _serviceMock.Setup(m => m.DeleteItemAluguelAsync(ItemAluguelId))
                        .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.DeleteItemAluguelAsync(ItemAluguelId);
            Assert.True(result);            

            _serviceMock = new Mock<IItemAluguelService>();
            _serviceMock.Setup(m => m.DeleteItemAluguelAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(false);
            _service = _serviceMock.Object;

            result = await _service.DeleteItemAluguelAsync(Guid.NewGuid());
            Assert.False(result);
        }
    }
}