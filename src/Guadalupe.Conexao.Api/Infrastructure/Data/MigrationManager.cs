using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Guadalupe.Conexao.Api.Infrastructure.Data
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope()) 
            {
                using (var context = scope.ServiceProvider.GetRequiredService<ConexaoContext>())
                {
                    context.Database.Migrate();
                }
            }

            ConexaoSeedData.Initialize(host.Services);

            return host;
        }
    }
}
