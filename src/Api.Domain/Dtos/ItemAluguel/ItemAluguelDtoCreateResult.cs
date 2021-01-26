using System;
using src.Api.Domain.Dtos.Filme;

namespace src.Api.Domain.Dtos.ItemAluguel
{
    public class ItemAluguelDtoCreateResult
    {
        public Guid Id { get; set; }
        public Guid AluguelId { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid FilmeId {get; set;}
    }
}