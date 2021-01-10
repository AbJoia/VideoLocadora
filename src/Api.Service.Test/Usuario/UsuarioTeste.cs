using System;
using System.Collections.Generic;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Enuns;

namespace src.Api.Service.Test.Usuario
{
    public class UsuarioTeste
    {
       public static Guid IdUsuario { get; set; } 
       public static string NomeUsuario { get; set; }
       public static string NomeAlterado { get; set; }
       public static string EmailUsuario { get; set; }
       public static string EmailAlterado { get; set; }
       public DateTime CreateAtUsuario { get; set; }
       public static TipoUsuario TipoUsuario { get; set; }
       public static IEnumerable<FilmeDtoGetResult> FilmesAlugados { get; set; }

       public UsuarioDto usuarioDto;
       public UsuarioDtoUpdate usuarioDtoUpdate;
       public UsuarioDtoCreateResult usuarioDtoCreateResult;
       public UsuarioDtoGetResult usuarioDtoGetResult;
       public IEnumerable<UsuarioDtoGetResult> usuarioDtoGetResultCollection;
       public UsuarioDtoLocacaoResult usuarioDtoLocacaoResult;
       public UsuarioDtoUpdateResult usuarioDtoUpdateResult;

       public UsuarioTeste()
       {
           IdUsuario = Guid.NewGuid();
           NomeUsuario = Faker.Name.FullName();
           EmailUsuario = Faker.Internet.Email();
           NomeAlterado = Faker.Name.FullName();
           CreateAtUsuario = DateTime.UtcNow;
           EmailAlterado = Faker.Internet.Email();           
           TipoUsuario = TipoUsuario.CLIENTE;
           FilmesAlugados = GetFilmesMock(new Random().Next(2, 5));           

           usuarioDto = new UsuarioDto()
           {
               Nome = NomeUsuario,
               Email = EmailUsuario
           };

           usuarioDtoUpdate = new UsuarioDtoUpdate()
           {
               Id = IdUsuario,
               Nome = NomeAlterado,
               Email = EmailAlterado,
           }; 

           usuarioDtoCreateResult = new UsuarioDtoCreateResult()
           {
               Id = IdUsuario,
               Nome = NomeUsuario,
               Email = EmailUsuario,
               CreateAt = CreateAtUsuario,
               TipoUsuario = TipoUsuario
           };

           usuarioDtoGetResult = new UsuarioDtoGetResult()
           {
               Id = IdUsuario,
               Nome = NomeUsuario,
               Email = EmailUsuario,
               TipoUsuario = TipoUsuario,
               CreateAt = CreateAtUsuario,
               FilmesAlugados = FilmesAlugados,
               UpdateAt = DateTime.UtcNow.AddHours(2.0),
           };

           usuarioDtoGetResultCollection = GetCollectionUsuarioResultMock(new Random().Next(2, 6));

           usuarioDtoLocacaoResult = new UsuarioDtoLocacaoResult()
           {
               Id = IdUsuario,
               Nome = NomeUsuario,
               Email = EmailUsuario,               
           };

           usuarioDtoUpdateResult = new UsuarioDtoUpdateResult()
           {
               Id = IdUsuario,
               Nome = NomeAlterado,
               Email = EmailAlterado,
               CreateAt = CreateAtUsuario,
               TipoUsuario = TipoUsuario,
               UpdateAt = DateTime.UtcNow.AddHours(2.0)
           }; 
       }

        private IEnumerable<UsuarioDtoGetResult> GetCollectionUsuarioResultMock(int v)
        {
            List<UsuarioDtoGetResult> listUsuarios = new List<UsuarioDtoGetResult>();
            for(int i=0; i<v; i++)
            {
                listUsuarios.Add(
                    new UsuarioDtoGetResult()
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        CreateAt = DateTime.UtcNow,
                        FilmesAlugados = GetFilmesMock(new Random().Next(0, 3)),
                        TipoUsuario = TipoUsuario,
                        UpdateAt = DateTime.UtcNow.AddHours(2.0),
                    }
                );
            }
            return listUsuarios;
        }

        private IEnumerable<FilmeDtoGetResult> GetFilmesMock(int v)
        {
           List<FilmeDtoGetResult> listFilmes = new List<FilmeDtoGetResult>();
           for(int i=0; i < v; i++)
           {
               listFilmes.Add(
                  new FilmeDtoGetResult()
                  {
                      Id = Guid.NewGuid(),
                      Titulo = Faker.Country.Name(),
                      Categoria = Categoria.AÇÃO,
                      QtdLocacao = Faker.RandomNumber.Next(2, 50),
                      CreateAt = DateTime.UtcNow,
                      UpdateAt = DateTime.UtcNow.AddHours(0.5),
                      Cadastrador = new FuncionarioDtoGetResult()
                        {
                            Id = Guid.NewGuid(),
                            Nome = Faker.Name.FullName(),
                            Email = Faker.Internet.Email(),
                            CreateAt = DateTime.UtcNow,
                            Matricula = new Random().Next(1000, 9999),
                        }                      
                  }  
               );
           }
           return listFilmes;
        }
    }
}