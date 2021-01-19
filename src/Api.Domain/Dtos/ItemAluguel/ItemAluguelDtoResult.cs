using System;
using src.Api.Domain.Dtos.Filme;

namespace src.Api.Domain.Dtos.ItemAluguel
{
    public class ItemAluguelDtoResult
    {
        public Guid Id { get; set; }
        public DateTime Createat { get; set; }
        public FilmeDtoLocacaoResult Filme {get; set;}
    }
}