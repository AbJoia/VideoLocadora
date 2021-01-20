using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Api.Domain.Dtos.Aluguel;

namespace src.Api.Domain.Interfaces.Services
{
    public interface IAluguelService
    {
        Task<AluguelDtoCompleteResult> GetCompleteByIdAsync (Guid id);
        Task<IEnumerable<AluguelDtoGetResult>> GetAllByUsuarioIdAsync (Guid usuarioId);
        Task<AluguelDtoCreateResult> PostAluguelAsync (AluguelDto aluguel);
        Task<AluguelDtoUpdateResult> PutAluguelAsync (AluguelDtoUpdate aluguel);
        Task<bool> DeleteAluguelAsync (Guid id);
    }
}