using System;
using src.Api.Domain.Dtos.Filme;

namespace src.Api.Domain.Dtos.ItemAluguel
{
    public class ItemAluguelDtoCreateResult
    {
        public Guid Id { get; set; }
        public Guid AluguelId { get; set; }
        public DateTime Createat { get; set; }
        public FilmeDtoLocacaoResult Filme {get; set;}
    }
}