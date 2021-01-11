using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Filme
{
    public class QuandoExecutarGetId : FilmeTeste
    {
        private IFilmeService _service;
        private Mock<IFilmeService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_GetId()
        {
            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.GetAsync(IdFilme))
                        .ReturnsAsync(filmeDtoGetResult);
            _service = _serviceMock.Object;

            var result = await _service.GetAsync(IdFilme);
            Assert.NotNull(result);            
            Assert.Equal(result.Id, IdFilme);
            Assert.Equal(result.Titulo, TituloFilme);
            Assert.Equal(result.Categoria, CategoriaFilme);
            Assert.Equal(result.Cadastrador, CadastradorFilme);
            Assert.Equal(result.QtdLocacao, QtdLocacaoFilme);            
            Assert.True(result.CreateAt != null);
            Assert.True(result.UpdateAt != null);            

             _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.GetAsync(It.IsAny<Guid>()))
                        .Returns(Task.FromResult<FilmeDtoGetResult>(null));
            _service = _serviceMock.Object;

            var nullResult = await _service.GetAsync(Guid.NewGuid());
            Assert.Null(nullResult);           
        }
    }
}