using System;

namespace src.Api.Domain.Dtos.Funcionario
{
    public class FuncionarioDtoGetResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }       
        public DateTime CreateAt { get; set; }        
        public long Matricula { get; set; }
    }
}