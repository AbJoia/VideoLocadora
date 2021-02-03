using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using src.Api.Domain.Dtos.Login;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces.Repositories;
using src.Api.Domain.Interfaces.Services;
using src.Api.Domain.Security;

namespace src.Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IFuncionarioRepository _repository;
        private SigningConfiguration _signingConfiguration;
        private TokenConfiguration _tokenConfiguration;
        private IConfiguration _configuration;

        public LoginService(IFuncionarioRepository repository,
                            SigningConfiguration signingConfiguration,
                            TokenConfiguration tokenConfiguration,
                            IConfiguration configuration)
        {
            _repository = repository;
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _configuration = configuration;            
        }

        public async Task<object> FindByLogin(LoginDto loginDto)
        {
           if(loginDto == null) return null;
           try
           {
               var result = await _repository.FindByLogin(loginDto);
               if(result == null || !loginDto.Email.Equals((result as FuncionarioEntity).Email))
               {
                   return new {
                       authenticated = false,
                       message = "Falha ao Autenticar! Usuário não encontrado."
                   };
               } 
               if(!GetHashSha256(loginDto.Senha)
                  .Equals((result as FuncionarioEntity).Senha))
                {
                    return new {
                       authenticated = false,
                       message = "Falha ao Autenticar! Senha incorreta."
                   };
                }

                var identity = new ClaimsIdentity(
                    new GenericIdentity((result as FuncionarioEntity).Nome),                    
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, loginDto.Email), 
                        new Claim("Role", "Funcionario")                       
                    }
                );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate.AddSeconds(_tokenConfiguration.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);
                              
                return SuccessObject(createDate, expirationDate, token, (result as FuncionarioEntity).Email);
           }
           catch (Exception e)
           {               
               throw e;
           }
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate,
                                     string token, string email)
        {
            return new 
            {
                authenticated = true,
                createDate = createDate,
                expirationDate = expirationDate,
                acessToken = token,
                userName = email,
                message = "Usuário logado com sucesso."                
            };
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate,
                                   DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(
                new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfiguration.Issuer,
                    Audience = _tokenConfiguration.Audience,
                    SigningCredentials = _signingConfiguration.SigningCredentials,
                    Subject = identity,
                    NotBefore = createDate,
                    Expires = expirationDate
                }
            );
            var token = handler.WriteToken(securityToken);
            return token;
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