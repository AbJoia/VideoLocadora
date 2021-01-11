using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Funcionario
{
    public class QuandoExecutarGetId : FuncionarioTeste
    {
        private IFuncionarioService _service;
        private Mock<IFuncionarioService> _servieMock;

        [Fact]
        public async Task E_Possivel_Executar_GetId()
        {
            _servieMock = new Mock<IFuncionarioService>();
            _servieMock.Setup(m => m.GetAsync(IdFuncionario))
                       .ReturnsAsync(funcionarioDtoGetResult); 
            _service = _servieMock.Object;

            var result = await _service.GetAsync(IdFuncionario);
            Assert.NotNull(result);
            Assert.Equal(result.Id, IdFuncionario);
            Assert.Equal(result.Nome, NomeFuncionario);
            Assert.Equal(result.Email, EmailFuncionario);
            Assert.Equal(result.Matricula, MatriculaFuncionario);
            Assert.True(result.CreateAt != null);

            var resultGuidEmpty = await _service.GetAsync(Guid.Empty);  
            Assert.Null(resultGuidEmpty);                  
        }         
    }
}