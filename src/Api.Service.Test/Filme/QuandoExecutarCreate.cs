using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Filme
{
    public class QuandoExecutarCreate : FilmeTeste
    {
        private IFilmeService _service;
        private Mock<IFilmeService> _serviceMock;

        [Fact]
        public async Task E_Possivel_Executar_Create()
        {
            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.PostAsync(filmeDto))
                        .ReturnsAsync(filmeDtoCreateResult);
            _service = _serviceMock.Object;

            var result = await _service.PostAsync(filmeDto);
            Assert.NotNull(result);
            Assert.True(result.Id != Guid.Empty);
            Assert.Equal(result.Titulo, TituloFilme);
            Assert.Equal(result.Categoria, CategoriaFilme);
            Assert.Equal(result.Funcionario, CadastradorFilme);
            Assert.Equal(result.Funcionario.Id, CadastradorFilme.Id);
            Assert.True(result.CreateAt != null); 

            _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.PostAsync(filmeDto))
                        .ReturnsAsync(filmeDtoCreateResult);
            _service = _serviceMock.Object;             

             _serviceMock = new Mock<IFilmeService>();
            _serviceMock.Setup(m => m.PostAsync(It.IsAny<FilmeDto>()))
                        .Returns(Task.FromResult<FilmeDtoCreateResult>(null));
            _service = _serviceMock.Object;

            var nullResult = await _service.PostAsync(filmeDto);
            Assert.Null(nullResult);           
        }        
    }
}