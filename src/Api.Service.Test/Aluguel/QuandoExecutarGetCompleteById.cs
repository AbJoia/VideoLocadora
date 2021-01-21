using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Aluguel
{
    public class QuandoExecutarGetCompleteById : AluguelTeste
    {
        private IAluguelService _service;
        private Mock<IAluguelService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_GetCompleteById()
        {
            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.GetCompleteByIdAsync(AluguelId))
                        .ReturnsAsync(aluguelDtoCompleteResult);
            _service = _serviceMock.Object;

            var result = await _service.GetCompleteByIdAsync(AluguelId);
            Assert.NotNull(result);
            Assert.NotNull(result.Usuario);
            Assert.NotNull(result.Itens);
            Assert.Equal(result.AluguelId, AluguelId);
            Assert.Equal(result.DataDevolucao, DataDevolucao);
            Assert.Equal(result.Usuario.Id, UsuarioId);
            Assert.Equal(result.Itens.Count(), aluguelDtoCompleteResult.Itens.Count());

            result = await _service.GetCompleteByIdAsync(Guid.NewGuid());
            Assert.Null(result); 

            _serviceMock = new Mock<IAluguelService>();
            _serviceMock.Setup(m => m.GetCompleteByIdAsync(It.IsAny<Guid>()))
                        .Returns(Task.FromResult<AluguelDtoCompleteResult>(null));
            _service = _serviceMock.Object;

            result = await _service.GetCompleteByIdAsync(Guid.NewGuid());
            Assert.Null(result);            
        }
    }
}