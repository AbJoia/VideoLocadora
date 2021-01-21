using System.Linq;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.ItemAluguel
{
    public class QuandoExecutarGetAllByAluguelId : ItemAluguelTeste
    {
        private IItemAluguelService _service;
        private Mock<IItemAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_GetAllItensByAluguelId()
        {
            _serviceMock = new Mock<IItemAluguelService>();
            _serviceMock.Setup(m => m.GetAllItensByAluguelIdAsync(AluguelId))
                        .ReturnsAsync(listItem);
            _service = _serviceMock.Object;

            var result = await _service.GetAllItensByAluguelIdAsync(AluguelId);
            Assert.NotNull(result);
            Assert.Equal(result.Count(), listItem.Count());                      
        }        
    }
}