using System.Collections.Generic;

namespace src.Api.Domain.Entities
{
    public class FuncionarioEntity : UsuarioEntity
    {
        public string Senha { get; set; }
        public long Matricula { get; set; }
        public IEnumerable<FilmeEntity> FilmesCadastrados { get; set; }
    }
}