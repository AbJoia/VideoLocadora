using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Api.Domain.Dtos.Filme;

namespace src.Api.Domain.Interfaces.Services
{
    public interface IFilmeService 
    {
        Task<FilmeDtoCreateResult> PostAsync (FilmeDto filme, Guid cadastrador);
        Task<FilmeDtoUpdateResult> PutAsync (FilmeDto filme, Guid cadastrador);
        Task<FilmeDtoGetResult> GetAsync (Guid id);
        Task<IEnumerable<FilmeDtoGetResult>> GetAllAsync ();
        Task<bool> DeleteAsync (Guid id);
        Task<FilmeDtoLocacaoResult> AluguelFilme (Guid IdLocatario, Guid IdFilme);       
    }
}