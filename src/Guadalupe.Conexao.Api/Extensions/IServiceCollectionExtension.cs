using Guadalupe.Conexao.Api.Domain;
using Guadalupe.Conexao.Api.Infrastructure.Data;
using Guadalupe.Conexao.Api.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Guadalupe.Conexao.Api.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddRepositories(this IServiceCollection service, IConfiguration configuration) 
        {
            service.AddDbContextPool<ConexaoContext>((contextOptions) =>
            {

                //TODO: Filtrar para habilitar somente em DEV.
                contextOptions.EnableSensitiveDataLogging();

                contextOptions.UseSqlServer(configuration.GetConnectionString("ConexaoDatabase"), (sqlOptions) => { 
                
                });
            });

            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<INoticeRepository, NoticeRepository>();

            ConexaoSeedData.Initialize(service.BuildServiceProvider());

        }
    }
}
