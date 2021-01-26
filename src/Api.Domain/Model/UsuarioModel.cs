using System;
using System.Collections.Generic;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Model
{
    public class UsuarioModel : BaseModel
    {     
        
        private string nome;
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        
        private TipoUsuario tipoUsuario;
        public TipoUsuario TipoUsuario
        {
            get { return tipoUsuario; }
            set {tipoUsuario = value;}
        }               
    }
}