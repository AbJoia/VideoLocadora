using System;
using System.Threading.Tasks;
using Moq;
using src.Api.Domain.Interfaces.Services;
using Xunit;

namespace src.Api.Service.Test.Funcionario
{
    public class QuandoExecutarCreate : FuncionarioTeste
    {
        private IFuncionarioService _service;
        private Mock<IFuncionarioService> _serviceMock;

       [Fact]
       public async Task E_Possivel_Realizar_Create()
       {
           _serviceMock = new Mock<IFuncionarioService>();
           _serviceMock.Setup(m => m.PostAsync(funcionarioDto))
                       .ReturnsAsync(funcionarioDtoCreateResult); 
           _service = _serviceMock.Object;

           var result = await _service.PostAsync(funcionarioDto);
           Assert.NotNull(result);
           Assert.True(result.Id != Guid.Empty);
           Assert.Equal(result.Id, IdFuncionario);
           Assert.Equal(result.Nome, NomeFuncionario);
           Assert.Equal(result.Email, EmailFuncionario);
           Assert.True(result.TipoUsuario == Domain.Enuns.TipoUsuario.FUNCIONARIO);
           Assert.True(result.Matricula != 0);                    
       }
    }
}