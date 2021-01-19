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
                BaseRepository<FuncionarioEntity> _funcionarioRepository = new BaseRepository<FuncionarioEntity>(context);
                
                
                var funcionario = await _funcionarioRepository.InsertAsync(
                    new FuncionarioEntity
                    {
                        Nome = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        Matricula = Faker.RandomNumber.Next(1000, 9999),
                        Senha = Faker.RandomNumber.Next(1000, 9999).ToString(),
                        TipoUsuario = TipoUsuario.FUNCIONARIO,                    
                    }
                );                               

                var filme = new FilmeEntity()
                {
                    Titulo = "Rocky Balboa V",
                    Categoria = Categoria.AÇÃO,                    
                    QtdLocacao = 0,
                    FuncionarioId = funcionario.Id                                                                                                                
                };

                //Insert
                var insertResult = await _repository.InsertAsync(filme);
                Assert.NotNull(insertResult);
                Assert.True(insertResult.Id != default(Guid));
                Assert.Equal(insertResult.Titulo, filme.Titulo);
                Assert.Equal(insertResult.Categoria, filme.Categoria);                
                Assert.Equal(insertResult.FuncionarioId, filme.FuncionarioId);
                Assert.Equal(insertResult.QtdLocacao, filme.QtdLocacao);               
                Assert.IsType<Categoria>(insertResult.Categoria);
                Assert.IsType<DateTime>(insertResult.CreateAt);

                //Update
                filme.Titulo = Faker.Country.Name();
                filme.QtdLocacao = Faker.RandomNumber.Next(1, 100);
                filme.Categoria = Categoria.SUSPENSE;               

                var updateResult = await _repository.UpdateAsync(filme);
                Assert.NotNull(updateResult);               
                Assert.True(updateResult.Id == filme.Id);
                Assert.Equal(updateResult.Titulo, filme.Titulo);
                Assert.Equal(updateResult.Categoria, filme.Categoria);                
                Assert.Equal(updateResult.FuncionarioId, filme.FuncionarioId);
                Assert.Equal(updateResult.QtdLocacao, filme.QtdLocacao);                               
                Assert.IsType<Categoria>(updateResult.Categoria);
                Assert.IsType<DateTime>(updateResult.CreateAt);
                Assert.True(updateResult.UpdateAt.CompareTo(filme.CreateAt) > 0);

                //GetId
                var getIdResul = await _repository.SelectAsync(filme.Id);
                Assert.NotNull(getIdResul);
                Assert.True(getIdResul.Id == updateResult.Id);
                Assert.Equal(getIdResul.Titulo, updateResult.Titulo);
                Assert.Equal(getIdResul.Categoria, updateResult.Categoria);                
                Assert.Equal(getIdResul.FuncionarioId, updateResult.FuncionarioId);
                Assert.Equal(getIdResul.QtdLocacao, updateResult.QtdLocacao);               

                //GetAll
                var getAllResult = await _repository.SelectAsync();
                Assert.NotNull(getAllResult);
                Assert.True(getAllResult.Count() > 0);
                Assert.True(getAllResult.Where(o => o.Id.Equals(filme.Id)).Count() == 1);

                //Delete
                var deleteResult = await _repository.DeleteAsync(updateResult.Id);
                Assert.True(deleteResult);  

            }
        }
    }
}