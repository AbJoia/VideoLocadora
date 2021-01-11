using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Funcionario
{
    public class QuandoExecutarUpdate : FuncionarioTeste
    {
        private IFuncionarioService _service;
        private Mock<IFuncionarioService> _servieMock;

        [Fact]
        public async Task E_Possivel_Executar_Update()
        {
            _servieMock = new Mock<IFuncionarioService>();
            _servieMock.Setup(m => m.PutAsync(funcionarioDtoUpdate))
                       .ReturnsAsync(funcionarioDtoUpdateResult); 
            _service = _servieMock.Object;

            var result = await _service.PutAsync(funcionarioDtoUpdate);
            Assert.NotNull(result);
            Assert.Equal(result.Id, IdFuncionario);
            Assert.Equal(result.Nome, NomeAlterado);
            Assert.Equal(result.Email, EmailAlterado);
            Assert.Equal(result.Matricula, MatriculaFuncionario);
            Assert.True(result.UpdateAt.CompareTo(result.CreateAt) > 0 );
            Assert.True(result.TipoUsuario == Domain.Enuns.TipoUsuario.FUNCIONARIO);

            _servieMock = new Mock<IFuncionarioService>();
            _servieMock.Setup(m => m.PutAsync(funcionarioDtoUpdate))
                       .Returns(Task.FromResult<FuncionarioDtoUpdateResult>(null)); 
            _service = _servieMock.Object;

            var nullResult = await _service.PutAsync(funcionarioDtoUpdate);
            Assert.Null(nullResult);
        }  
    }
}