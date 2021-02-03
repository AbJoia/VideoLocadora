using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces;
using src.Api.Domain.Interfaces.Services;
using src.Api.Domain.Model;

namespace src.Api.Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepository<UsuarioEntity> _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;

        public UsuarioService(IRepository<UsuarioEntity> repository,
                              IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UsuarioDtoGetResult>> GetAllAsync()
        {
            var result = await _repository.SelectAsync();
            if(result == null) return null;
            return _mapper.Map<IEnumerable<UsuarioDtoGetResult>>(result); 
        }

        public async Task<UsuarioDtoGetResult> GetAsync(Guid id)
        {
            var result = await _repository.SelectAsync(id);
            if(result == null) return null;
            return _mapper.Map<UsuarioDtoGetResult>(result);
        }

        public async Task<UsuarioDtoCreateResult> PostAsync(UsuarioDto usuario)
        {
            var model = _mapper.Map<UsuarioModel>(usuario);
            model.TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE;
            var entity = _mapper.Map<UsuarioEntity>(model);
            var result = await _repository.InsertAsync(entity);
            if(result == null) return null;
            SendMail(result);
            return _mapper.Map<UsuarioDtoCreateResult>(result);
        }       

        public async Task<UsuarioDtoUpdateResult> PutAsync(UsuarioDtoUpdate usuario)
        {
            var model = _mapper.Map<UsuarioModel>(usuario);
            model.TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE;
            var entity = _mapper.Map<UsuarioEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            if(result == null) return null;
            return _mapper.Map<UsuarioDtoUpdateResult>(result);
        }

        private void SendMail(UsuarioEntity result)
        { 
            try
            {
                var nome =  Environment.GetEnvironmentVariable("NomeLoja");
                var mailAddress =  Environment.GetEnvironmentVariable("mailAddress");
                var password = Environment.GetEnvironmentVariable("mailPassword");

                MailMessage mail = new MailMessage();                
                mail.From = new MailAddress(mailAddress);

                mail.CC.Add(result.Email);            
                mail.Subject = $"Comprovante de Cadastro da {nome}";
                mail.IsBodyHtml = true;                
                mail.Body = $"<h3>Esta é sua confirmação de cadastro na {nome}.</h3></br>"
                            + $"<p><b>Nome:</b> {result.Nome}</p>"
                            + $"<p><b>Email:</b> {result.Email}</p>"
                            + $"<p><b>Data de Cadastro:</b> {result.CreateAt}</p>"
                            + $"<p><b>Seja bem vindo!</b></p>"
                            +"<img src='https://mobizoo.com.br/wp-content/uploads/2020/03/assistir-filmes-online-gratis.jpg' width='500' height='200'/>";

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);                
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mailAddress, password);            
                client.Send(mail);
            }
            catch (System.Exception e)
            {                
                throw e;
            }
        }
    }
}