using System.Collections.Generic;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Entities
{
    public class UsuarioEntity : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public IEnumerable<FilmeEntity> FilmesAlugados { get; set; }
    }
}