using Microsoft.Extensions.DependencyInjection;
using System;

namespace Guadalupe.Conexao.Api.Infrastructure.Data
{
    public static class ConexaoSeedData
    {
        public static void Initialize(IServiceProvider service) 
        {
            using (var scope = service.CreateScope()) 
            {
                var context = scope.ServiceProvider.GetRequiredService<ConexaoContext>();

                context.Database.EnsureCreated();
            }
        }
    }
}
