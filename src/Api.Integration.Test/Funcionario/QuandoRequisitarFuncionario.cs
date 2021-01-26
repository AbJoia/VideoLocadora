using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using src.Api.Domain.Dtos.Funcionario;
using Xunit;

namespace src.Api.Integration.Test.Funcionario
{
    public class QuandoRequisitarFuncionario : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Executar_Crud_Funcionario()
        {               
            #region Post

            var funcionarioDto = new FuncionarioDto
            {
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Senha = new Random().Next(1000, 9999).ToString()
            };

            var response = await PostJsonAsync(funcionarioDto, $"{HostApi}funcionarios", Client);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var postResult = JsonConvert.DeserializeObject<FuncionarioDtoCreateResult>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            Assert.Equal(postResult.Nome, funcionarioDto.Nome);
            Assert.Equal(postResult.Email, funcionarioDto.Email);
            Assert.True(postResult.TipoUsuario == Domain.Enuns.TipoUsuario.FUNCIONARIO);
            Assert.True(postResult.Matricula != default(long));
            Assert.True(postResult.Id != default(Guid));

            #endregion

            #region Put

            var funcionarioDtoUpdate = new FuncionarioDtoUpdate
            {
                Id = postResult.Id,
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Senha = new Random().Next(1000, 9999).ToString()                
            };

            response = await Client.PutAsync($"{HostApi}funcionarios",
                        new StringContent(JsonConvert.SerializeObject(funcionarioDtoUpdate),
                                          Encoding.UTF8, "application/json"));

            jsonResult = await response.Content.ReadAsStringAsync();
            
            var putResult = JsonConvert.DeserializeObject<FuncionarioDtoUpdateResult>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(putResult.UpdateAt.CompareTo(postResult.CreateAt) > 0);
            Assert.Equal(putResult.Id, postResult.Id);            
            Assert.Equal(putResult.TipoUsuario, postResult.TipoUsuario);
            Assert.NotEqual(putResult.Nome, postResult.Nome);
            Assert.NotEqual(putResult.Email, postResult.Email);
            Assert.Equal(putResult.Matricula, postResult.Matricula); 

            #endregion

            #region GetById

            response = await Client.GetAsync($"{HostApi}funcionarios/{postResult.Id}");
            jsonResult = await response.Content.ReadAsStringAsync();
            var getResult = JsonConvert.DeserializeObject<FuncionarioDtoUpdateResult>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.Equal(getResult.Id, putResult.Id);
            Assert.Equal(getResult.Email, putResult.Email);
            Assert.Equal(getResult.Nome, putResult.Nome);
            Assert.Equal(getResult.TipoUsuario, putResult.TipoUsuario);
            Assert.Equal(getResult.Matricula, putResult.Matricula); 

            #endregion
            
            #region GetAll

            response = await Client.GetAsync($"{HostApi}funcionarios");
            jsonResult = await response.Content.ReadAsStringAsync();
            var getAllResult = JsonConvert
                               .DeserializeObject<IEnumerable<FuncionarioDtoGetResult>>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(getAllResult.Count() > 0);
            Assert.True(getAllResult.Where(u => u.Id == postResult.Id).Count() == 1);

            #endregion
            
            #region Delete

            response = await Client.DeleteAsync($"{HostApi}funcionarios/{postResult.Id}");
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            jsonResult = await response.Content.ReadAsStringAsync();
            var deleteResult = JsonConvert.DeserializeObject<bool>(jsonResult);
            Assert.True(deleteResult);

            //GetId Confirmação Delete
            response = await Client.GetAsync($"{HostApi}funcionarios/{postResult.Id}");
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);

            #endregion
        } 
        
    }
}