using System;
using System.ComponentModel.DataAnnotations;

namespace src.Api.Domain.Dtos.ItemAluguel
{
    public class ItemAluguelDto
    {
        [Required(ErrorMessage = "Campo aluguel Id Obrigatório.")]
        public Guid AluguelId { get; set; }

        [Required(ErrorMessage = "Campo filme Obrigatório.")]
        public Guid FilmeId { get; set; }        
    }
}