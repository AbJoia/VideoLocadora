using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using src.Api.Data.Context;
using src.Api.Data.Implementation;
using src.Api.Data.Repository;
using src.Api.Domain.Entities;
using Xunit;

namespace src.Api.Data.Test
{
    public class ItemAluguelCrudCompleto : BaseTeste, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public ItemAluguelCrudCompleto(DbTest dbtest)
        {
            _serviceProvider = dbtest.serviceProvider;            
        }

        [Fact]
        public async Task E_Possivel_Executar_Crud_Item_Aluguel()
        {
            using(var context = _serviceProvider.GetService<MyContext>())
            {
                BaseRepository<UsuarioEntity> _usuarioRepository = new BaseRepository<UsuarioEntity>(context);
                BaseRepository<FuncionarioEntity> _funcionarioRepository = new BaseRepository<FuncionarioEntity>(context);
                BaseRepository<FilmeEntity> _filmeRepository = new BaseRepository<FilmeEntity>(context);
                AluguelImplementation _aluguelRepository = new AluguelImplementation(context);
                ItemAluguelImplementation _itemAluguelRepository = new ItemAluguelImplementation(context);

                var cliente = await _usuarioRepository.InsertAsync(
                     new UsuarioEntity
                    {
                        Nome = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,                        
                    }
                );

                var funcionario = await _funcionarioRepository.InsertAsync(
                    new FuncionarioEntity
                    {
                        Nome = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),                        
                        Senha = Faker.RandomNumber.Next(1000, 9999).ToString(),
                        Matricula = Faker.RandomNumber.Next(1000, 9999), 
                        TipoUsuario = Domain.Enuns.TipoUsuario.FUNCIONARIO,                        
                    }
                );

                var filme = await _filmeRepository.InsertAsync(
                    new FilmeEntity
                    {
                        Titulo = Faker.Country.Name(),
                        Categoria = Domain.Enuns.Categoria.SUSPENSE,
                        FuncionarioId = funcionario.Id,
                        QtdLocacao = 0,                        
                    }
                );

                var filme2 = await _filmeRepository.InsertAsync(
                    new FilmeEntity
                    {
                        Titulo = Faker.Country.Name(),
                        Categoria = Domain.Enuns.Categoria.SUSPENSE,
                        FuncionarioId = funcionario.Id,
                        QtdLocacao = 0,                        
                    }
                );

                var aluguel = await _aluguelRepository.RealizarAluguel(
                    new AluguelEntity
                    {
                        UsuarioId = cliente.Id,
                        DataDevolucao = DateTime.UtcNow.AddHours(72.0),
                    }
                );

                var itemAluguel = new ItemAluguelEntity
                                {
                                    AluguelId = aluguel.Id,
                                    FilmeId = filme.Id,                        
                                };

                var itemAluguel2 = new ItemAluguelEntity
                                {
                                    AluguelId = aluguel.Id,
                                    FilmeId = filme.Id,                        
                                };

                //Post
                var postResult = await _itemAluguelRepository.InsertAsync(itemAluguel);
                Assert.NotNull(postResult);
                Assert.Equal(postResult.AluguelId, aluguel.Id);
                Assert.Equal(postResult.FilmeId, filme.Id);
                Assert.NotNull(postResult.Aluguel);
                Assert.NotNull(postResult.Aluguel.Usuario);
                Assert.NotNull(postResult.Filme);
                Assert.NotNull(postResult.Filme.Funcionario);

                //Put
                itemAluguel.FilmeId = filme2.Id;
                var putResult = await _itemAluguelRepository.UpdateAsync(itemAluguel);
                Assert.NotNull(putResult);
                Assert.Equal(putResult.AluguelId, aluguel.Id);
                Assert.Equal(putResult.FilmeId, filme2.Id);
                Assert.NotNull(putResult.Aluguel);
                Assert.NotNull(putResult.Aluguel.Usuario);
                Assert.NotNull(putResult.Filme);
                Assert.NotNull(putResult.Filme.Funcionario);

                //GetAllItensByAluguelId
                await _itemAluguelRepository.InsertAsync(itemAluguel2);
                var getAllByAluguelId = await _itemAluguelRepository.GetAllItensByAluguelId(aluguel.Id);
                Assert.NotNull(getAllByAluguelId);
                Assert.True(getAllByAluguelId.Where(i => i.AluguelId == aluguel.Id).Count() == 2);

                //GetById
                var getById = await _itemAluguelRepository.SelectAsync(itemAluguel.Id);
                Assert.NotNull(getById);
                Assert.NotNull(getById.Filme);
                Assert.NotNull(getById.Aluguel);
                Assert.Equal(getById.FilmeId, itemAluguel.FilmeId);
                Assert.Equal(getById.AluguelId, itemAluguel.AluguelId);

                //GetAll
                var getAll = await _itemAluguelRepository.SelectAsync();
                Assert.NotNull(getAll);
                Assert.True(getAll.Count() > 0);                

                //Delete
                var deleteResult = await _itemAluguelRepository.DeleteAsync(itemAluguel2.Id);
                Assert.True(deleteResult);
                
            }
        }
    }
}