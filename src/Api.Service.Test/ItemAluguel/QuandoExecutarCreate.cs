using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.ItemAluguel;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.ItemAluguel
{
    public class QuandoExecutarCreate : ItemAluguelTeste
    {
        private IItemAluguelService _service;
        private Mock<IItemAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_Create()
        {
            _serviceMock = new Mock<IItemAluguelService>();
            _serviceMock.Setup(m => m.PostItemAluguelAsync(itemAluguelDto))
                        .ReturnsAsync(dtoCreateResult);
            _service = _serviceMock.Object;

            var result = await _service.PostItemAluguelAsync(itemAluguelDto);
            Assert.NotNull(result);
            Assert.NotNull(result.Filme);
            Assert.True(result.Createat != null);
            Assert.Equal(result.Id, ItemAluguelId);
            Assert.Equal(result.AluguelId, AluguelId);
            Assert.Equal(result.Filme.Id, FilmeId);

            _serviceMock = new Mock<IItemAluguelService>();
            _serviceMock.Setup(m => m.PostItemAluguelAsync(It.IsAny<ItemAluguelDto>()))
                        .Returns(Task.FromResult<ItemAluguelDtoCreateResult>(null));
            _service = _serviceMock.Object;

            result = await _service.PostItemAluguelAsync(itemAluguelDto);
            Assert.Null(result);
        }        
    }
}