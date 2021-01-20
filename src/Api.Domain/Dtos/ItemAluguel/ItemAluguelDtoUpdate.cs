using System;
using System.ComponentModel.DataAnnotations;

namespace src.Api.Domain.Dtos.ItemAluguel
{
    public class ItemAluguelDtoUpdate
    {
        [Required(ErrorMessage = "Campo Id é Obrigatório.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo aluguel Id é Obrigatório.")]
        public Guid AluguelId { get; set; }

        [Required(ErrorMessage = "Campo filme é Obrigatório.")]
        public Guid FilmeId { get; set; }  
    }
}