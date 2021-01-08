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
    public class UsuarioFuncionarioCrudCompleto : BaseTeste, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioFuncionarioCrudCompleto(DbTest dbTest)
        {
            _serviceProvider = dbTest.serviceProvider;            
        }

        [Fact]
        public async Task E_Possivel_Realizar_Crud_Funcionario()
        {
            using(var context = _serviceProvider.GetService<MyContext>())
            {
                BaseRepository<FuncionarioEntity> _repository = new BaseRepository<FuncionarioEntity>(context);

                var funcionario = new FuncionarioEntity()
                {
                    Nome = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    Matricula = Faker.RandomNumber.Next(1000, 99999),
                    Senha = Faker.RandomNumber.Next(1000, 99999).ToString(),
                    TipoUsuario = TipoUsuario.FUNCIONARIO,                    
                };

                //Insert
                var insertResult = await _repository.InsertAsync(funcionario);
                Assert.NotNull(insertResult);
                Assert.Equal(insertResult.Nome, funcionario.Nome); 
                Assert.Equal(insertResult.Email, funcionario.Email); 
                Assert.Equal(insertResult.Matricula, funcionario.Matricula);
                Assert.Equal(insertResult.Senha, funcionario.Senha); 
                Assert.Equal(insertResult.TipoUsuario, funcionario.TipoUsuario);  
                Assert.IsType<TipoUsuario>(insertResult.TipoUsuario);                

                //Update                
                funcionario.Nome = Faker.Name.FullName();
                funcionario.Email = Faker.Internet.Email();
                funcionario.Matricula = Faker.RandomNumber.Next(1000, 99999);
                funcionario.Senha = Faker.RandomNumber.Next(1000, 99999).ToString();

                var updateResult = await _repository.UpdateAsync(funcionario);
                Assert.NotNull(updateResult);
                Assert.Equal(updateResult.Id, funcionario.Id);
                Assert.Equal(updateResult.Nome, funcionario.Nome);
                Assert.Equal(updateResult.Email, funcionario.Email);                
                Assert.Equal(updateResult.Matricula, funcionario.Matricula);
                Assert.Equal(updateResult.Senha, funcionario.Senha);
                Assert.True(updateResult.UpdateAt.CompareTo(funcionario.CreateAt) > 0);
                Assert.IsType<TipoUsuario>(updateResult.TipoUsuario); 

                //GetId
                var getIdResult = await _repository.SelectAsync(funcionario.Id); 
                Assert.NotNull(getIdResult);
                Assert.Equal(getIdResult.Id, funcionario.Id);
                Assert.Equal(getIdResult.Nome, funcionario.Nome);
                Assert.Equal(getIdResult.Email, funcionario.Email);                
                Assert.Equal(getIdResult.Matricula, funcionario.Matricula);
                Assert.Equal(getIdResult.Senha, funcionario.Senha);

                //GetAll
                var getAllResult = await _repository.SelectAsync();
                Assert.NotNull(getAllResult);
                Assert.True(getAllResult.Count() > 0);

                //Delete
                var deleteResult = await _repository.DeleteAsync(funcionario.Id);
                Assert.True(deleteResult); 
                
            }
        }
        
    }
}