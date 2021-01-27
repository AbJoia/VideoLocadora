using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using src.Api.Data.Context;
using src.Api.Data.Implementation;
using src.Api.Data.Repository;
using src.Api.Domain.Interfaces;
using src.Api.Domain.Interfaces.Repositories;

namespace src.Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IAluguelRepository, AluguelImplementation>();
            serviceCollection.AddScoped<IItemAluguelRepository, ItemAluguelImplementation>();
            serviceCollection.AddScoped<IFuncionarioRepository, FuncionarioImplementation>();
            serviceCollection.AddDbContext<MyContext>(options =>
                options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION"))
            );
        }
    }
}