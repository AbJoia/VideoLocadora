using System.ComponentModel.DataAnnotations;

namespace src.Api.Domain.Dtos.Login
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Campo Email é Obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um Email Valido")]
        [StringLength(100, ErrorMessage = "Email deve ter no máximo {1} caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo senha é obrigatório")]
        [StringLength(6, ErrorMessage = "Campo senha deve ter no máximo {1} caracteres")]
        public string Senha { get; set; }
    }
}