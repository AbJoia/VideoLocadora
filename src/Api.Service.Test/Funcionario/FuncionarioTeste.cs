using System;
using System.Collections.Generic;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Enuns;

namespace src.Api.Service.Test.Funcionario
{
    public class FuncionarioTeste
    {
        public Guid IdFuncionario { get; set; }
        public string NomeFuncionario { get; set; }
        public string NomeAlterado { get; set; }
        public string EmailFuncionario { get; set; }
        public string EmailAlterado { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public IEnumerable<FilmeDtoGetResult> FilmesAlugadosFuncionario { get; set; }
        public string SenhaFuncionario { get; set; }
        public string SenhaAlterado { get; set; }
        public long MatriculaFuncionario { get; set; }
        public long MatriculaAlterado { get; set; }
        public IEnumerable<FilmeDtoGetResult> FilmesCadastradosFuncionario { get; set; }

        public IEnumerable<FuncionarioDtoGetResult> listaFuncionarios;
        public FuncionarioDto funcionarioDto;
        public FuncionarioDtoCreateResult funcionarioDtoCreateResult;
        public FuncionarioDtoGetResult funcionarioDtoGetResult;
        public FuncionarioDtoUpdate funcionarioDtoUpdate;
        public FuncionarioDtoUpdateResult funcionarioDtoUpdateResult;

        public FuncionarioTeste()
        {
            IdFuncionario = Guid.NewGuid();
            NomeFuncionario = Faker.Name.FullName();
            EmailFuncionario = Faker.Internet.Email();           
            FilmesAlugadosFuncionario = GetFilmesMock(new Random().Next(2, 4), false);
            SenhaFuncionario = Faker.RandomNumber.Next(1000, 9999).ToString();
            MatriculaFuncionario = Faker.RandomNumber.Next(1000, 9999);
            FilmesCadastradosFuncionario = GetFilmesMock(new Random().Next(2, 4), true);
            listaFuncionarios = GetFuncionariosMock(new Random().Next(2, 4));

            funcionarioDto = new FuncionarioDto()
            {
                Nome = NomeFuncionario,
                Email = EmailFuncionario,
                Senha = SenhaFuncionario,                
            };

            funcionarioDtoCreateResult = new FuncionarioDtoCreateResult()
            {
                Id = IdFuncionario,
                Nome = NomeFuncionario,
                Email = EmailFuncionario,
                Matricula = MatriculaFuncionario,
                CreateAt = DateTime.UtcNow,
                TipoUsuario = TipoUsuario.FUNCIONARIO,
            };

            funcionarioDtoGetResult = new FuncionarioDtoGetResult()
            {
                Id = IdFuncionario,
                Nome = NomeFuncionario,
                Email = EmailFuncionario,
                Matricula = MatriculaFuncionario,
                CreateAt = DateTime.UtcNow,
            };

            funcionarioDtoUpdate = new FuncionarioDtoUpdate()
            {
                Id = IdFuncionario,
                Nome = NomeAlterado,
                Email = EmailAlterado,
                Senha = SenhaAlterado
            };

            funcionarioDtoUpdateResult = new FuncionarioDtoUpdateResult()
            {
                Id = IdFuncionario,
                Nome = NomeAlterado,
                Email = EmailAlterado,
                Matricula = MatriculaFuncionario,
                TipoUsuario = TipoUsuario.FUNCIONARIO,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow.AddDays(2.0),
            };            
        }

        private IEnumerable<FuncionarioDtoGetResult> GetFuncionariosMock(int v)
        {
            List<FuncionarioDtoGetResult> list = new List<FuncionarioDtoGetResult>();
            for(int i=0; i<v; i++)
            {
                list.Add(
                   new FuncionarioDtoGetResult()
                   {
                       Id = Guid.NewGuid(),
                       Nome = Faker.Name.FullName(),
                       Email = Faker.Internet.Email(),
                       Matricula = Faker.RandomNumber.Next(1000, 9999),
                       CreateAt = DateTime.UtcNow,
                   }     
                );
            }
            return list;
        }

        private IEnumerable<FilmeDtoGetResult> GetFilmesMock(int v, bool filmeCadastrado)
        {            
            List<FilmeDtoGetResult> filmes = new List<FilmeDtoGetResult>();
            for(int i=0; i<v; i++ )
            {
                filmes.Add(
                  new FilmeDtoGetResult()
                  {
                      Id = Guid.NewGuid(),
                      Titulo = Faker.Name.FullName(),
                      Categoria = (Categoria) new Random().Next(Enum.GetNames(typeof(Categoria)).Length),
                      Cadastrador = new FuncionarioDtoGetResult()
                        {
                           Id = filmeCadastrado? IdFuncionario : Guid.NewGuid(),
                           Nome = filmeCadastrado? NomeFuncionario : Faker.Name.FullName(),
                           Email = filmeCadastrado? EmailFuncionario : Faker.Internet.Email(),
                           Matricula = filmeCadastrado? MatriculaFuncionario : Faker.RandomNumber.Next(1000, 9999),
                           CreateAt = DateTime.UtcNow,
                        },
                    CreateAt = DateTime.UtcNow,
                    QtdLocacao = Faker.RandomNumber.Next(2, 10),
                    UpdateAt = DateTime.UtcNow.AddDays(2.0)
                  }  
                );
            }
            return filmes; 
        }
    }
}