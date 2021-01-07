using System.Collections.Generic;

namespace src.Api.Domain.Entities
{
    public class FuncionarioEntity : UsuarioEntity
    {
        public string senha { get; set; }
        public long matricula { get; set; }
        public IEnumerable<FilmeEntity> FilmesCadastrados { get; set; }
    }
}