using System;
using System.Collections.Generic;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Entities;
using src.Api.Domain.Enuns;

namespace src.Api.Service.Test.Filme
{
    public class FilmeTeste
    {
        public Guid IdFilme { get; set; }
        public string TituloFilme { get; set; }
        public string TituloFilmeAlterado { get; set; }
        public Categoria CategoriaFilme { get; set; }
        public Categoria CategoriaFilmeAlterado { get; set; }
        public int QtdLocacaoFilme { get; set; }  
        public UsuarioDtoLocacaoResult LocatarioFilme { get; set; } 
        public FuncionarioDtoGetResult CadastradorFilme { get; set; } 

        public FilmeDto filmeDto;
        public FilmeDtoCreateResult filmeDtoCreateResult;
        public FilmeDtoGetResult filmeDtoGetResult;
        public IEnumerable<FilmeDtoGetResult> listaFilmes;
        public FilmeDtoLocacaoResult filmeDtoLocacaoResult;
        public FilmeDtoUpdate filmeDtoUpdate;
        public FilmeDtoUpdateResult filmeDtoUpdateResult;

        public FilmeTeste()
        {
            IdFilme = Guid.NewGuid();
            TituloFilme = Faker.Country.Name();
            TituloFilmeAlterado = Faker.Country.Name();
            CategoriaFilme = (Categoria) new Random().Next(Enum.GetNames(typeof(Categoria)).Length); 
            CategoriaFilmeAlterado = (Categoria) new Random().Next(Enum.GetNames(typeof(Categoria)).Length);
            QtdLocacaoFilme = new Random().Next(0, 50);
            LocatarioFilme = GetLocatarioFilmeMock();
            CadastradorFilme = GetCadastradorFilmeMock();
            
            filmeDto = new FilmeDto()
            {
                Titulo = TituloFilme,
                Categoria = CategoriaFilme
            };

            filmeDtoCreateResult = new FilmeDtoCreateResult()
            {
                Id = IdFilme,
                Titulo = TituloFilme,
                Funcionario = CadastradorFilme,
                Categoria = CategoriaFilme,
                CreateAt = DateTime.UtcNow
            };

            filmeDtoGetResult = new FilmeDtoGetResult()
            {
                Id = IdFilme,
                Titulo = TituloFilme,
                Funcionario = CadastradorFilme,
                Categoria = CategoriaFilme,
                CreateAt = DateTime.UtcNow,
                QtdLocacao = QtdLocacaoFilme,
                UpdateAt = DateTime.UtcNow.AddDays(3.0)
            };

            listaFilmes = GetFilmesMock(new Random().Next(1, 10));

            filmeDtoLocacaoResult = new FilmeDtoLocacaoResult()
            {
                Id = IdFilme,
                Titulo = TituloFilme,
                Categoria = CategoriaFilme,                
            };

            filmeDtoUpdate = new FilmeDtoUpdate()
            {
                Id = IdFilme,
                Titulo = TituloFilmeAlterado,
                Categoria = CategoriaFilmeAlterado
            };

            filmeDtoUpdateResult = new FilmeDtoUpdateResult()
            {
                Id = IdFilme,
                Titulo = TituloFilmeAlterado,
                Categoria = CategoriaFilmeAlterado,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow.AddHours(2.0),
                Funcionario = CadastradorFilme,      
            };           
        }

        private UsuarioDtoLocacaoResult GetLocatarioFilmeMock()
        {
            return new UsuarioDtoLocacaoResult()
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Name.FullName(),
                        Email = Faker.Internet.Email()    
                    };
        }

        private IEnumerable<FilmeDtoGetResult> GetFilmesMock(int v)
        {
            List<FilmeDtoGetResult> filmes = new List<FilmeDtoGetResult>();
            for(int i=0; i<v; i++)
            {
                filmes.Add(
                   new FilmeDtoGetResult()
                   {
                       Id = Guid.NewGuid(),
                       Titulo = Faker.Country.Name(),
                       Funcionario = GetCadastradorFilmeMock(),
                       Categoria = (Categoria) new Random().Next(Enum.GetNames(typeof(Categoria)).Length),
                       CreateAt = DateTime.UtcNow,
                       QtdLocacao = new Random().Next(1, 10),
                       UpdateAt = DateTime.UtcNow.AddDays(3.0)
                   }     
                );
            }
            return filmes;
        }

        private FuncionarioDtoGetResult GetCadastradorFilmeMock()
        {
            return new FuncionarioDtoGetResult()
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        Matricula = new Random().Next(1000, 9999),
                        CreateAt = DateTime.UtcNow 
                    };
        }
    }
}