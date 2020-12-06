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

                var @new = context.Database.EnsureCreated();

                if (@new) 
                {
                    var renan = new Domain.Person("renan@hotmail.com");
                    var renanUser = new Domain.User(renan);

                    var ana = new Domain.Person("anacbogaz@hotmail.com")
                        .AdicionarProfileImage("Imagens/profile.jpg");

                    var noticeByAna = new Domain.Notice("1ª mensagem teste - banco de dados.", "Imagens/notice.jpg", ana);

                    context.User.Add(renanUser);
                    context.Person.Add(ana);
                    context.Notice.Add(noticeByAna);

                    context.SaveChanges();
                }
            }
        }
    }
}
