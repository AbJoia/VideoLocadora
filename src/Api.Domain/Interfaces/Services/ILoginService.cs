using System.Threading.Tasks;
using src.Api.Domain.Dtos.Login;

namespace src.Api.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task<object> FindByLogin (LoginDto loginDto);
    }
}