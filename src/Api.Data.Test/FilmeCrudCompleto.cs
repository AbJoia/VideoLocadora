using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using src.Api.Data.Context;
using src.Api.Data.Repository;
using src.Api.Domain.Entities;
using src.Api.Domain.Enuns;
using Xunit;

namespace src.Api.Data.Test
{
    public class FilmeCrudCompleto : BaseTeste, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public FilmeCrudCompleto(DbTest dbTest)
        {
            _serviceProvider = dbTest.serviceProvider;
        }

        [Fact]
        public async Task E_Possivel_Realizar_Crud_Filme()
        {
            using(var context = _serviceProvider.GetService<MyContext>())
            {
                BaseRepository<FilmeEntity> _repository = new BaseRepository<FilmeEntity>(context);
                
                var funcionario = new FuncionarioEntity()
                {
                    Nome = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    Matricula = Faker.RandomNumber.Next(1000, 9999),
                    Senha = Faker.RandomNumber.Next(1000, 9999).ToString(),
                    TipoUsuario = TipoUsuario.FUNCIONARIO,
                };

                var locatario = new UsuarioEntity()
                {
                    Nome = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    TipoUsuario = TipoUsuario.CLIENTE,                    
                };

                var filme = new FilmeEntity()
                {
                    Titulo = Faker.Country.Name(),
                    Categoria = Categoria.AÇÃO,
                    Cadastrador = funcionario,
                    QtdLocacao = 0
                };

                //Insert
                var insertResult = await _repository.InsertAsync(filme);
                Assert.NotNull(insertResult);
                Assert.True(insertResult.Id != Guid.Empty);
                Assert.Equal(insertResult.Titulo, filme.Titulo);
                Assert.Equal(insertResult.Categoria, filme.Categoria);                
                Assert.Equal(insertResult.Cadastrador, filme.Cadastrador);
                Assert.Equal(insertResult.QtdLocacao, filme.QtdLocacao);
                Assert.IsType<FuncionarioEntity>(insertResult.Cadastrador); 
                Assert.IsType<Categoria>(insertResult.Categoria);
                Assert.IsType<DateTime>(insertResult.CreateAt);

                //Update
                filme.Titulo = Faker.Country.Name();
                filme.QtdLocacao = Faker.RandomNumber.Next(1, 100);
                filme.Categoria = Categoria.SUSPENSE;
                filme.Cadastrador = new FuncionarioEntity(){
                    Nome = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    Matricula = Faker.RandomNumber.Next(1000, 9999),
                    Senha = Faker.RandomNumber.Next(1000, 9999).ToString(),
                    TipoUsuario = TipoUsuario.FUNCIONARIO,
                };
                filme.Locatario = locatario;

                var updateResult = await _repository.UpdateAsync(filme);
                Assert.NotNull(updateResult);               
                Assert.True(updateResult.Id == filme.Id);
                Assert.Equal(updateResult.Titulo, filme.Titulo);
                Assert.Equal(updateResult.Categoria, filme.Categoria);                
                Assert.Equal(updateResult.Cadastrador, filme.Cadastrador);
                Assert.Equal(updateResult.QtdLocacao, filme.QtdLocacao);
                Assert.Equal(updateResult.Locatario, filme.Locatario);
                Assert.IsType<FuncionarioEntity>(updateResult.Cadastrador); 
                Assert.IsType<Categoria>(updateResult.Categoria);
                Assert.IsType<DateTime>(updateResult.CreateAt);
                Assert.True(updateResult.UpdateAt.CompareTo(filme.CreateAt) > 0);

                //GetId
                var getIdResul = await _repository.SelectAsync(filme.Id);
                Assert.NotNull(getIdResul);
                Assert.True(getIdResul.Id == updateResult.Id);
                Assert.Equal(getIdResul.Titulo, updateResult.Titulo);
                Assert.Equal(getIdResul.Categoria, updateResult.Categoria);                
                Assert.Equal(getIdResul.Cadastrador, updateResult.Cadastrador);
                Assert.Equal(getIdResul.QtdLocacao, updateResult.QtdLocacao);
                Assert.Equal(getIdResul.Locatario, updateResult.Locatario);

                //GetAll
                var getAllResult = await _repository.SelectAsync();
                Assert.NotNull(getAllResult);
                Assert.True(getAllResult.Count() > 0);

                //Delete
                var deleteResult = await _repository.DeleteAsync(updateResult.Id);
                Assert.True(deleteResult);  

            }
        }
    }
}