using System;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Model
{
    public class FilmeModel : BaseModel
    {
                
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

        private Guid funcionarioId;
        public Guid FuncionarioId
        {
            get { return funcionarioId; }
            set { funcionarioId = value; }
        }          
    }
}