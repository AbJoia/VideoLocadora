using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Api.Data.Context;
using src.Api.Data.Repository;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces.Repositories;

namespace src.Api.Data.Implementation
{
    public class AluguelImplementation : BaseRepository<AluguelEntity>, IAluguelRepository
    {
        private DbSet<AluguelEntity> _dataSet;

        private MyContext _context;

        public AluguelImplementation(MyContext context) : base(context)
        {
            _dataSet = context.Set<AluguelEntity>();
            _context = context;
        }

        public async Task<IEnumerable<AluguelEntity>> GetAllByUsuarioId(Guid usuarioId)
        {
            try
            {
                if(usuarioId == null) return null;
                var result = await _dataSet.Include(a => a.Usuario)
                                           .Where(a => a.Usuario.Id == usuarioId)
                                           .ToListAsync();
                if(result == null) return null;
                return result;
            }
            catch (Exception e)
            {                
                throw e;
            }
        }

        public async Task<AluguelEntity> GetCompleteById(Guid id)
        {
            try
            {
                if(id == default(Guid)) return null;
                var result = await _dataSet.Include(a => a.Usuario)
                                           .Include(a => a.ItensAluguel)
                                           .SingleOrDefaultAsync(a => a.Id.Equals(id));
                if(result == null) return null;
                return result;
            }
            catch (Exception e)
            {                
                throw e;
            }
        }

        public async Task<AluguelEntity> RealizarAluguel(AluguelEntity aluguel)
        {
            try
            {
                if(aluguel == null) return null;                
                if(aluguel.UsuarioId == null) return null;
                if(aluguel.Id == Guid.Empty)
                {
                    aluguel.Id = Guid.NewGuid();
                }
                aluguel.CreateAt = DateTime.UtcNow;
                await _dataSet.AddAsync(aluguel);
                await _context.SaveChangesAsync();
                return aluguel;
                
            }
            catch (Exception e)
            {                
                throw e;
            }
        }
    }
}