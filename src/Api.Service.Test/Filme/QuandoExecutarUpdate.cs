using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Filme
{
    public class QuandoExecutarUpdate : FilmeTeste
    {
        private IFilmeService _service;
        private Mock<IFilmeService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_Update()
        {
            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.PutAsync(filmeDtoUpdate))
                        .ReturnsAsync(filmeDtoUpdateResult);
            _service = _serviceMock.Object;

            var result = await _service.PutAsync(filmeDtoUpdate);
            Assert.NotNull(result);
            Assert.True(result.Id != Guid.Empty);
            Assert.Equal(result.Id, IdFilme);
            Assert.Equal(result.Titulo, TituloFilmeAlterado);            
            Assert.Equal(result.Categoria, CategoriaFilmeAlterado);
            Assert.Equal(result.Funcionario, CadastradorFilme);
            Assert.Equal(result.Funcionario.Id, CadastradorFilme.Id);
            Assert.True(result.CreateAt != null);
            Assert.True(result.UpdateAt.CompareTo(result.CreateAt) > 0); 

            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.PutAsync(filmeDtoUpdate))
                        .ReturnsAsync(filmeDtoUpdateResult);
            _service = _serviceMock.Object;          

             _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.PutAsync(It.IsAny<FilmeDtoUpdate>()))
                        .Returns(Task.FromResult<FilmeDtoUpdateResult>(null));
            _service = _serviceMock.Object;

            var nullResult = await _service.PutAsync(filmeDtoUpdate);
            Assert.Null(nullResult);           
        }
    }
}