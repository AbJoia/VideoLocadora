using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Funcionario
{
    public class QuandoExecutarDelete : FuncionarioTeste
    {
        private IFuncionarioService _service;
        private Mock<IFuncionarioService> _servieMock;

        [Fact]
        public async Task E_Possivel_Executar_GetId()
        {
            _servieMock = new Mock<IFuncionarioService>();
            _servieMock.Setup(m => m.DeleteAsync(IdFuncionario))
                       .ReturnsAsync(true); 
            _service = _servieMock.Object;

            var result = await _service.DeleteAsync(IdFuncionario);            
            Assert.True(result);

            var resultGuidEmpty = await _service.DeleteAsync(Guid.Empty);  
            Assert.False(resultGuidEmpty);                  
        }  
        
    }
}