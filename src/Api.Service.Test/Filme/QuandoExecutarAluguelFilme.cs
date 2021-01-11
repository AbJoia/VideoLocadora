using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Filme
{
    public class QuandoExecutarAluguelFilme : FilmeTeste
    {
        private IFilmeService _service;
        private Mock<IFilmeService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_AluguelFilme()
        {
            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.AluguelFilmeAsync(LocatarioFilme.Id, IdFilme))
                        .ReturnsAsync(filmeDtoLocacaoResult);
            _service = _serviceMock.Object;

            var result = await _service.AluguelFilmeAsync(LocatarioFilme.Id, IdFilme);
            Assert.NotNull(result);
            Assert.Equal(result.Id, IdFilme);
            Assert.Equal(result.Categoria, CategoriaFilme);
            Assert.Equal(result.Titulo, TituloFilme);
            Assert.Equal(result.Locatario, LocatarioFilme);

            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.AluguelFilmeAsync(LocatarioFilme.Id, IdFilme))
                        .ReturnsAsync(filmeDtoLocacaoResult);
            _service = _serviceMock.Object;

            var nullResult = await _service.AluguelFilmeAsync(Guid.Empty, IdFilme);
            Assert.Null(nullResult);

            nullResult = await _service.AluguelFilmeAsync(LocatarioFilme.Id, Guid.Empty);
            Assert.Null(nullResult);

            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.AluguelFilmeAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                        .Returns(Task.FromResult<FilmeDtoLocacaoResult>(null));
            _service = _serviceMock.Object;

            nullResult = await _service.AluguelFilmeAsync(Guid.NewGuid(), Guid.NewGuid());
            Assert.Null(nullResult);
        }        
    }
}