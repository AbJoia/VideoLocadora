using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Entities
{
    public class UsuarioEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [EnumDataType(typeof(TipoUsuario))]
        public TipoUsuario TipoUsuario { get; set; }
        
        public IEnumerable<AluguelEntity> Alugueis { get; set; }
    }
}