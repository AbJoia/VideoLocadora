using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Api.Domain.Dtos.ItemAluguel;

namespace src.Api.Domain.Interfaces.Services
{
    public interface IItemAluguelService
    {
        Task<IEnumerable<ItemAluguelDtoGetResult>> GetAllItensByAluguelIdAsync (Guid aluguelId);
        Task<ItemAluguelDtoCreateResult> PostItemAluguelAsync (ItemAluguelDto item);
        Task<ItemAluguelDtoUpdateResult> PutItemAluguelAsync (ItemAluguelDtoUpdate item);
        Task<bool> DeleteItemAluguelAsync (Guid id);
    }
}