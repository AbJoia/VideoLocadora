using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Api.Domain.Dtos.Filme;

namespace src.Api.Domain.Interfaces.Services
{
    public interface IFilmeService 
    {
        Task<FilmeDtoCreateResult> PostAsync (FilmeDto filme);
        Task<FilmeDtoUpdateResult> PutAsync (FilmeDto filme);
        Task<FilmeDtoGetResult> GetAsync (Guid id);
        Task<IEnumerable<FilmeDtoGetResult>> GetAllAsync ();
        Task<bool> DeleteAsync (Guid id);
    }
}