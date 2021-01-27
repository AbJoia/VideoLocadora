using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using src.Api.Domain.Dtos.Login;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces.Repositories;
using src.Api.Domain.Interfaces.Services;

namespace src.Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IFuncionarioRepository _repository;

        public LoginService(IFuncionarioRepository repository)
        {
            _repository = repository;            
        }

        public async Task<object> FindByLogin(LoginDto loginDto)
        {
           if(loginDto == null) return null;
           try
           {
               var result = await _repository.FindByLogin(loginDto);
               if(result == null) return null;
               if(!GetHashSha256(loginDto.Senha)
                  .Equals((result as FuncionarioEntity).Senha)) return null;
               if(!loginDto.Email.Equals((result as FuncionarioEntity).Email)) return null;
               return result;
           }
           catch (Exception e)
           {               
               throw e;
           }
        }        

        private string GetHashSha256(string senha)
        {
            using(SHA256 sHA256 = SHA256.Create())
            {
                byte[] bytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(senha));
                StringBuilder builder = new StringBuilder();
                for(int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}