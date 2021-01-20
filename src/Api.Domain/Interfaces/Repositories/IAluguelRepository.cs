using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Entities;

namespace src.Api.Domain.Interfaces.Repositories
{
    public interface IAluguelRepository : IRepository<AluguelEntity>
    {
        Task<AluguelEntity> RealizarAluguel (AluguelEntity aluguel);
        Task<IEnumerable<AluguelEntity>> GetAllByUsuarioId (Guid usuarioId);
        Task<AluguelEntity> GetCompleteById (Guid id);
    }
}