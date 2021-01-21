using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Aluguel
{
    public class QuandoExecutarGetAllByUsuarioId : AluguelTeste
    {
        private IAluguelService _service;
        private Mock<IAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_GetAllByUsuarioId()
        {
            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.GetAllByUsuarioIdAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(alugueis);
            _service = _serviceMock.Object;

            var result = await _service.GetAllByUsuarioIdAsync(Guid.NewGuid());
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
            Assert.Equal(result.Count(), alugueis.Count());                       
        }        
    }
}