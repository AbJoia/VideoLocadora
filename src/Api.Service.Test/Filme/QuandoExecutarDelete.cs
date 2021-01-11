using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Filme
{
    public class QuandoExecutarDelete : FilmeTeste
    {
        private IFilmeService _service;
        private Mock<IFilmeService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_Delete()
        {
            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.DeleteAsync(IdFilme);
            Assert.True(result);

            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.DeleteAsync(IdFilme))
                        .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var nullResult = await _service.DeleteAsync(Guid.NewGuid());
            Assert.False(nullResult);

            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.DeleteAsync(It.IsAny<Guid>()))
                        .ReturnsAsync(false);
            _service = _serviceMock.Object;

            var falseResult = await _service.DeleteAsync(Guid.NewGuid());
            Assert.False(falseResult);                               
        }        
    }
}