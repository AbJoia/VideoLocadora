using System;
using src.Api.Domain.Dtos.Filme;

namespace src.Api.Domain.Dtos.ItemAluguel
{
    public class ItemAluguelDtoUpdateResult
    {
        public Guid Id { get; set; }
        public DateTime UpdateAt { get; set; }
        public Guid FilmeId {get; set;}
    }
}