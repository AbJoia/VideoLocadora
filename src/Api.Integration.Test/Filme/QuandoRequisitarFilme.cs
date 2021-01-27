using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Enuns;
using Xunit;

namespace src.Api.Integration.Test.Filme
{
    public class QuandoRequisitarFilme : BaseIntegration  
    {
        [Fact]
        public async Task E_Possivel_Executar_Crud_Filme()
        {               
            #region Post

            var funcionario = new FuncionarioDto
            {
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Senha = Faker.RandomNumber.Next(1000, 9999).ToString()
            };

            var response = await PostJsonAsync(funcionario, $"{HostApi}funcionarios", Client);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var funcionarioResult = JsonConvert.DeserializeObject<FuncionarioDtoCreateResult>(jsonResult); 

            var filmeDto = new FilmeDto
            {
                Titulo = Faker.Name.FullName(),
                Categoria = (Categoria) new Random().Next(Enum.GetNames(typeof(Categoria)).Length),
                FuncionarioId = funcionarioResult.Id
            };

            response = await PostJsonAsync(filmeDto, $"{HostApi}filmes", Client);
            jsonResult = await response.Content.ReadAsStringAsync();
            var postResult = JsonConvert.DeserializeObject<FilmeDtoCreateResult>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            Assert.Equal(postResult.Titulo, filmeDto.Titulo);            
            Assert.IsType<Categoria>(postResult.Categoria);            
            Assert.True(postResult.Id != default(Guid));
            Assert.Equal(postResult.FuncionarioId, filmeDto.FuncionarioId);

            #endregion

            #region Put

            var filmeDtoUpdate = new FilmeDtoUpdate
            {
                Id = postResult.Id,
                Titulo = Faker.Name.FullName(),
                Categoria = (Categoria) new Random().Next(Enum.GetNames(typeof(Categoria)).Length),
                FuncionarioId = funcionarioResult.Id                             
            };

            response = await Client.PutAsync($"{HostApi}filmes",
                        new StringContent(JsonConvert.SerializeObject(filmeDtoUpdate),
                                          Encoding.UTF8, "application/json"));

            jsonResult = await response.Content.ReadAsStringAsync();
            
            var putResult = JsonConvert.DeserializeObject<FilmeDtoUpdateResult>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(putResult.UpdateAt.CompareTo(postResult.CreateAt) > 0);
            Assert.Equal(putResult.Id, postResult.Id);            
            Assert.IsType<Categoria>(putResult.Categoria);
            Assert.NotEqual(putResult.Titulo, postResult.Titulo);
            Assert.Null(putResult.Funcionario);
            
            #endregion

            #region GetById

            response = await Client.GetAsync($"{HostApi}filmes/{postResult.Id}");
            jsonResult = await response.Content.ReadAsStringAsync();
            var getResult = JsonConvert.DeserializeObject<FilmeDtoGetResult>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.Equal(getResult.Id, putResult.Id);
            Assert.Equal(getResult.Titulo, putResult.Titulo);            
            Assert.Equal(getResult.Categoria, putResult.Categoria);
            Assert.Equal(getResult.FuncionarioId, postResult.FuncionarioId);            

            #endregion
            
            #region GetAll

            response = await Client.GetAsync($"{HostApi}filmes");
            jsonResult = await response.Content.ReadAsStringAsync();
            var getAllResult = JsonConvert
                               .DeserializeObject<IEnumerable<FilmeDtoGetResult>>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(getAllResult.Count() > 0);
            Assert.True(getAllResult.Where(u => u.Id == postResult.Id).Count() == 1);

            #endregion
            
            #region Delete

            response = await Client.DeleteAsync($"{HostApi}filmes/{postResult.Id}");
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            jsonResult = await response.Content.ReadAsStringAsync();
            var deleteResult = JsonConvert.DeserializeObject<bool>(jsonResult);
            Assert.True(deleteResult);

            //GetId Confirmação Delete
            response = await Client.GetAsync($"{HostApi}filmes/{postResult.Id}");
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);

            #endregion
        }        
    }
}