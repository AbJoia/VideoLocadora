using Microsoft.Extensions.DependencyInjection;
using src.Api.Domain.Interfaces.Services;
using src.Api.Service.Services;

namespace src.Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesServices(IServiceCollection service)
        {
            service.AddTransient<IAluguelService, AluguelService>();
            service.AddTransient<IFilmeService, FilmeService>();
            service.AddTransient<IFuncionarioService, FuncionarioService>();
            service.AddTransient<IItemAluguelService, ItemAluguelService>();
            service.AddTransient<IUsuarioService, UsuarioService>();
        }
    }
}