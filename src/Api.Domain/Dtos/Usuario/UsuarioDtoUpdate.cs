using System;
using System.ComponentModel.DataAnnotations;

namespace src.Api.Domain.Dtos.Usuario
{
    public class UsuarioDtoUpdate
    {
        [Required(ErrorMessage = "Campo Id é obrigatório.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Campo nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "Campo nome deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        [StringLength(100, ErrorMessage = "Campo email deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }
    }
}