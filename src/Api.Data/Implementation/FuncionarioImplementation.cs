using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Api.Data.Context;
using src.Api.Data.Repository;
using src.Api.Domain.Dtos.Login;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces.Repositories;

namespace src.Api.Data.Implementation
{
    public class FuncionarioImplementation : BaseRepository<FuncionarioEntity>, IFuncionarioRepository
    {
        private DbSet<FuncionarioEntity> _dataSet;

        public FuncionarioImplementation(MyContext context) : base (context)
        {
            _dataSet = context.Set<FuncionarioEntity>();            
        }

        public async Task<object> FindByLogin(LoginDto loginDto)
        {
            if(loginDto == null || 
               string.IsNullOrWhiteSpace(loginDto.Email) ||
               string.IsNullOrWhiteSpace(loginDto.Senha))
            {
                return null;
            } 

            try
            {
                return await _dataSet.SingleOrDefaultAsync(f => f.Email.Equals(loginDto.Email));
            }
            catch (Exception e)
            {                
                throw e;
            }
        }
    }
}