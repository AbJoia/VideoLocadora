using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using src.Api.Domain.Dtos.Usuario;
using Xunit;

namespace src.Api.Integration.Test.Usuario
{
    public class QuandoRequisitarUsuario : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Executar_Crud_Usuario()
        {               
            #region Post

            var usuarioDto = new UsuarioDto
            {
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            var response = await PostJsonAsync(usuarioDto, $"{HostApi}usuarios", Client);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var postResult = JsonConvert.DeserializeObject<UsuarioDtoCreateResult>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            Assert.Equal(postResult.Nome, usuarioDto.Nome);
            Assert.Equal(postResult.Email, usuarioDto.Email);
            Assert.True(postResult.TipoUsuario == Domain.Enuns.TipoUsuario.CLIENTE);
            Assert.True(postResult.Id != default(Guid));

            #endregion

            #region Put

            var usuarioDtoUpdate = new UsuarioDtoUpdate
            {
                Id = postResult.Id,
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            response = await Client.PutAsync($"{HostApi}usuarios",
                        new StringContent(JsonConvert.SerializeObject(usuarioDtoUpdate),
                                          Encoding.UTF8, "application/json"));

            jsonResult = await response.Content.ReadAsStringAsync();
            
            var putResult = JsonConvert.DeserializeObject<UsuarioDtoUpdateResult>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(putResult.UpdateAt.CompareTo(postResult.CreateAt) > 0);
            Assert.Equal(putResult.Id, postResult.Id);            
            Assert.Equal(putResult.TipoUsuario, postResult.TipoUsuario);
            Assert.NotEqual(putResult.Nome, postResult.Nome);
            Assert.NotEqual(putResult.Email, postResult.Email); 

            #endregion

            #region GetById

            response = await Client.GetAsync($"{HostApi}usuarios/{postResult.Id}");
            jsonResult = await response.Content.ReadAsStringAsync();
            var getResult = JsonConvert.DeserializeObject<UsuarioDtoGetResult>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.Equal(getResult.Id, putResult.Id);
            Assert.Equal(getResult.Email, putResult.Email);
            Assert.Equal(getResult.Nome, putResult.Nome);
            Assert.Equal(getResult.TipoUsuario, putResult.TipoUsuario); 

            #endregion
            
            #region GetAll

            response = await Client.GetAsync($"{HostApi}usuarios");
            jsonResult = await response.Content.ReadAsStringAsync();
            var getAllResult = JsonConvert
                               .DeserializeObject<IEnumerable<UsuarioDtoGetResult>>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(getAllResult.Count() > 0);
            Assert.True(getAllResult.Where(u => u.Id == postResult.Id).Count() == 1);

            #endregion
            
            #region Delete

            response = await Client.DeleteAsync($"{HostApi}usuarios/{postResult.Id}");
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            jsonResult = await response.Content.ReadAsStringAsync();
            var deleteResult = JsonConvert.DeserializeObject<bool>(jsonResult);
            Assert.True(deleteResult);

            //GetId Confirmação Delete
            response = await Client.GetAsync($"{HostApi}usuarios/{postResult.Id}");
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);

            #endregion
        }        
    }
}