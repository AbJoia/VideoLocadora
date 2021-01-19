using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Api.Data.Context;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces;

namespace src.Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MyContext _context;
        private DbSet<T> _dataSet;

        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataSet = context.Set<T>();            
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                if(id == Guid.Empty) return false;
                var result = await _dataSet.SingleOrDefaultAsync(t => t.Id.Equals(id));
                if(result == null) return false;
                _dataSet.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }            
        }

        public async Task<T> InsertAsync(T item)
        {
           try
           {
               if(item == null) return null;

               if(item.Id == Guid.Empty)
               {
                   item.Id = Guid.NewGuid();
               }

               item.CreateAt = DateTime.UtcNow;

               await _dataSet.AddAsync(item);
               await _context.SaveChangesAsync();
               return item;
           }
           catch (Exception e)
           {               
               throw e;
           }
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                if(id == Guid.Empty) return null;
                var result = await _dataSet.SingleOrDefaultAsync(t => t.Id.Equals(id));
                if(result == null) return null;
                return result;  
            }
            catch (Exception e)
            {                
                throw e;
            }            
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {            
            try
            {
                return await _dataSet.ToListAsync();
            }
            catch (Exception e)
            {                
                throw e;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {            
            try
            {
               if(item == null) return null;
               var result = await _dataSet.SingleOrDefaultAsync(t => t.Id.Equals(item.Id));
               if(result == null) return null;
               item.CreateAt = result.CreateAt;
               item.UpdateAt = DateTime.UtcNow;
               _context.Entry(result).CurrentValues.SetValues(item);
               await _context.SaveChangesAsync(); 
               return item;
            }
            catch (Exception e)
            {                
                throw e;
            }            
        }
    }
}