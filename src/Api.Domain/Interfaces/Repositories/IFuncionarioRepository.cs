using System.Threading.Tasks;
using src.Api.Domain.Dtos.Login;
using src.Api.Domain.Entities;

namespace src.Api.Domain.Interfaces.Repositories
{
    public interface IFuncionarioRepository : IRepository<FuncionarioEntity>
    {
        Task<object> FindByLogin (LoginDto loginDto);
    }
}