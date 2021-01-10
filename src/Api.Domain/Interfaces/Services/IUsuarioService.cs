using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Api.Domain.Dtos.Usuario;

namespace src.Api.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDtoCreateResult> PostAsync (UsuarioDto usuario);
        Task<UsuarioDtoUpdateResult> PutAsync (UsuarioDtoUpdate usuario);
        Task<UsuarioDtoGetResult> GetAsync (Guid id);
        Task<IEnumerable<UsuarioDtoGetResult>> GetAllAsync ();
        Task<bool> DeleteAsync (Guid id);        
    }
}