using Guadalupe.Conexao.Api.Domain;
using Guadalupe.Conexao.Api.Infrastructure.Data;
using Guadalupe.Conexao.Api.Infrastructure.Data.Repositories;
using Guadalupe.Conexao.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Guadalupe.Conexao.Api.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddRepositories(this IServiceCollection service, IConfiguration configuration, bool isDevelopEnvironment) 
        {
            service.AddDbContextPool<ConexaoContext>((contextOptions) =>
            {
                if(isDevelopEnvironment) 
                {
                    contextOptions.EnableSensitiveDataLogging();
                }
                
                contextOptions.UseSqlServer(configuration.GetConnectionString("RelationalDB"));
            });

            service.AddScoped<IIdentityService, IdentityService>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<INoticeRepository, NoticeRepository>();
            service.AddSingleton<ISmtpService, SmtpService>();
            service.AddSingleton<INotificationService, NotificationService>();
        }
    }
}
