using System;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Model
{
    public class FilmeModel
    {
        private Guid id;
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }
        
        private DateTime createAt;
        public DateTime CreateAt
        {
            get { return createAt; }
            set { createAt = value == null? DateTime.UtcNow : value; }
        }

        private DateTime updateAt;
        public DateTime UpdateAt
        {
            get { return updateAt; }
            set { updateAt = value; }
        }
        
        private string titulo;
        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }
        
        private Categoria categoria;
        public Categoria Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
       
        private int qtdLocacao;
        public int QtdLocacao
        {
            get { return qtdLocacao; }
            set { qtdLocacao = value; }
        }
        
        private UsuarioModel usuario;
        public UsuarioModel Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        
        private FuncionarioModel funcionario;
        public FuncionarioModel Funcionario
        {
            get { return funcionario; }
            set { funcionario = value; }
        }       
    }
}