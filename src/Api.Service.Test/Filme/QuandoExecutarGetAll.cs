using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Filme
{
    public class QuandoExecutarGetAll : FilmeTeste
    {
        private IFilmeService _service;
        private Mock<IFilmeService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_GetId()
        {
            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.GetAllAsync())
                        .ReturnsAsync(listaFilmes);
            _service = _serviceMock.Object;

            var result = await _service.GetAllAsync();
            Assert.NotNull(result);                        
            Assert.True(result.Count() > 0);
            Assert.True(result.Count() >= 2 && result.Count() <= 4);                     
        }        
    }
}