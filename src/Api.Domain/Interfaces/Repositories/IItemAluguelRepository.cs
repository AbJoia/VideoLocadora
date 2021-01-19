using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Api.Domain.Entities;

namespace src.Api.Domain.Interfaces.Repositories
{
    public interface IItemAluguelRepository : IRepository<ItemAluguelEntity>
    {
       Task<IEnumerable<ItemAluguelEntity>> GetAllItensByAluguelId(Guid id); 
    }
}