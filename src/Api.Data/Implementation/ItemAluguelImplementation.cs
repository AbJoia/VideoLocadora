using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Api.Data.Context;
using src.Api.Data.Repository;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces.Repositories;

namespace src.Api.Data.Implementation
{
    public class ItemAluguelImplementation : BaseRepository<ItemAluguelEntity>, IItemAluguelRepository
    {
        private DbSet<ItemAluguelEntity> _dataSet;
        public ItemAluguelImplementation(MyContext context) : base(context)
        {
            _dataSet = context.Set<ItemAluguelEntity>();            
        }

        public async Task<IEnumerable<ItemAluguelEntity>> GetAllItensByAluguelId(Guid id)
        {
            return await _dataSet.Include(i => i.Filme).ToListAsync();
        }
    }
}