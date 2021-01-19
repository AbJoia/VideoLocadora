using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Entities
{
    public class FilmeEntity : BaseEntity
    {
        [Required]
        [MaxLength(60)]
        public string Titulo { get; set; }

        [Required]
        [EnumDataType(typeof(Categoria))]
        public Categoria Categoria { get; set; }

        [Required]
        public int QtdLocacao { get; set; }

        public IEnumerable<ItemAluguelEntity> ItensAluguel {get; set;}           

        [Required]
        public Guid FuncionarioId { get; set; }       
        
        public FuncionarioEntity Funcionario { get; set; }     
    }
}