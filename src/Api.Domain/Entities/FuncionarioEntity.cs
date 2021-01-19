using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace src.Api.Domain.Entities
{
    public class FuncionarioEntity : UsuarioEntity
    {
        [Required]
        [MaxLength(200)]
        public string Senha { get; set; }

        [Required]
        public long Matricula { get; set; }
        
        public IEnumerable<FilmeEntity> FilmesCadastrados { get; set; }
    }
}