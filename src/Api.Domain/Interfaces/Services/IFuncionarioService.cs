using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Api.Domain.Dtos.Funcionario;

namespace src.Api.Domain.Interfaces.Services
{
    public interface IFuncionarioService
    {
        Task<FuncionarioDtoCreateResult> PostAsync (FuncionarioDto funcionario);
        Task<FuncionarioDtoUpdateResult> PutAsync (FuncionarioDto funcionario);
        Task<FuncionarioDtoGetResult> GetAsync (Guid id);
        Task<IEnumerable<FuncionarioDtoGetResult>> GetAllAsync ();
        Task<bool> DeleteAsync (Guid id);
    }
}