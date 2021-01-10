using System;
using System.ComponentModel.DataAnnotations;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Dtos.Filme
{
    public class FilmeDtoUpdate
    {
        [Required(ErrorMessage = "Campo Id é obrigatório.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Campo titulo é obrigatório")]
        [StringLength(150, ErrorMessage = "Campo titulo deve ter no máximo {1} caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo categoria é obrigatório")]
        [EnumDataType(typeof(Categoria))]
        public Categoria Categoria { get; set; }
    }
}