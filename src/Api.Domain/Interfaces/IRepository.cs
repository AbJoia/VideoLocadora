using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Api.Domain.Entities;

namespace src.Api.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<T> SelectAsync(Guid id);
        IEnumerable<Task<T>> SelectAsync();
        bool DeleteAsync(Guid id);
    }
}