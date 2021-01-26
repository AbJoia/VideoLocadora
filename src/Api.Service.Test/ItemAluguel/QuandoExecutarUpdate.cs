using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.ItemAluguel;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.ItemAluguel
{
    public class QuandoExecutarUpdate : ItemAluguelTeste
    {
        private IItemAluguelService _service;
        private Mock<IItemAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_Update()
        {
            _serviceMock = new Mock<IItemAluguelService>();
            _serviceMock.Setup(m => m.PutItemAluguelAsync(dtoUpdate))
                        .ReturnsAsync(dtoUpdateResult);
            _service = _serviceMock.Object;

            var result = await _service.PutItemAluguelAsync(dtoUpdate);
            Assert.NotNull(result);            
            Assert.True(result.UpdateAt != null);            
            Assert.Equal(result.Id, ItemAluguelId);
            Assert.NotEqual(result.FilmeId, FilmeId);            

            _serviceMock = new Mock<IItemAluguelService>();
            _serviceMock.Setup(m => m.PutItemAluguelAsync(It.IsAny<ItemAluguelDtoUpdate>()))
                        .Returns(Task.FromResult<ItemAluguelDtoUpdateResult>(null));
            _service = _serviceMock.Object;

            result = await _service.PutItemAluguelAsync(dtoUpdate);
            Assert.Null(result);
        }        
    }
}