using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace src.Api.Domain.Entities
{
    public class AluguelEntity : BaseEntity
    {
        [Required]
        public Guid UsuarioId { get; set; }

        public UsuarioEntity Usuario { get; set; } 

        [Required]
        public IEnumerable<ItemAluguelEntity> ItensAluguel { get; set; }

        public DateTime DataDevolucao { get; set; }
    }
}