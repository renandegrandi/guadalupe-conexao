using Guadalupe.Conexao.Api.Domain;
using Guadalupe.Conexao.Api.Infrastructure.Data;
using Guadalupe.Conexao.Api.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Guadalupe.Conexao.Api.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddRepositories(this IServiceCollection service) 
        {
            service.AddDbContextPool<ConexaoContext>((options) =>
            {

            });

            service.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
