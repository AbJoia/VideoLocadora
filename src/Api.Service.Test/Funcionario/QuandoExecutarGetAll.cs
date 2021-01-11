using System.Linq;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Funcionario
{
    public class QuandoExecutarGetAll : FuncionarioTeste
    {
        private IFuncionarioService _service;
        private Mock<IFuncionarioService> _servieMock;

        [Fact]
        public async Task E_Possivel_Executar_GetAll()
        {
            _servieMock = new Mock<IFuncionarioService>();
            _servieMock.Setup(m => m.GetAllAsync())
                       .ReturnsAsync(listaFuncionarios); 
            _service = _servieMock.Object;

            var result = await _service.GetAllAsync();
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
            Assert.True(result.Count() >= 2 && result.Count() <= 4);                             
        } 
    }
}