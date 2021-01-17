using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Guadalupe.Conexao.Api.Infrastructure.Data
{
    public static class ConexaoSeedData
    {
        public static void Initialize(IServiceProvider service)
        {
            var noticeOne = new Guid("f6e6545f-1713-4ef4-a276-9156f4076392");
            var noticeTwo = new Guid("9fad4e29-0cd4-4302-b359-a668de05e628");
            var noticeThree = new Guid("6972fdbe-35e8-41d6-a216-49c20bc173ac");
            var noticeFour = new Guid("32371a18-b9d3-45f2-8cdb-a7f24f7943e6");
            var noticeFive = new Guid("f6a89841-f9fd-4a11-8849-02dc53afb952");

            using (var scope = service.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<ConexaoContext>())
                {
                    var missaoPerson = new Domain.Person(new Guid("736b8b7d-b74f-4925-987f-6a6f25b8a856"), "guadalupe.conexao@gmail.com", "Missão Guadalupe")
                        .AdicionarProfileImage("Imagens/profile.jpg");

                    if (!context.Person.Any((p) => p.Id == missaoPerson.Id))
                        context.Person.Add(missaoPerson);

                    if (!context.User.Any((b) => b.Person.Id == missaoPerson.Id))
                        context.User.Add(new Domain.User(missaoPerson));

                    if (!context.Notice.Any((q) => q.Id == noticeOne))
                        context.Notice.Add(new Domain.Notice(noticeOne, "O bom samaritano", "O Bom Samaritano \nHomenagem aos 90 anos do nosso amado Padre Carlos", "Imagens/imagem21.jpg")
                        .AddPostedBy(missaoPerson)
                        .ModifyRegistrationDate(new DateTime(2020, 11, 27)));

                    if (!context.Notice.Any((q) => q.Id == noticeTwo))
                        context.Notice.Add(new Domain.Notice(noticeTwo, "Grupo de oração", "Mais uma semana abençoada está começando e hoje é dia de Grupo de Oração.\n" +
                    "Você não pode perder!A participação especial desta noite é da Mariana Faria, e a sua presença lá conosco é muito importante. Te esperamos, às 20h, na Igreja Maceno.As vagas são limitadas e por ordem de chegada.Antes, às 19h, tem Missa.\n" +
                    "Convide a família e os amigos. A transmissão ao vivo pelo YouTube também continua.", "Imagens/imagem19.jpg")
                        .AddPostedBy(missaoPerson)
                        .ModifyRegistrationDate(new DateTime(2020, 11, 09)));

                    if (!context.Notice.Any((q) => q.Id == noticeThree))
                        context.Notice.Add(new Domain.Notice(noticeThree, "Terço de Misericórdia", "Terço da Misericórdia", "Imagens/imagem18.jpg")
                        .AddPostedBy(missaoPerson)
                        .ModifyRegistrationDate(new DateTime(2020, 11, 28)));

                    if (!context.Notice.Any((q) => q.Id == noticeFour))
                        context.Notice.Add(new Domain.Notice(noticeFour, "É pizza!!", "Quando pessoas com o coração cheio de amor se reúnem para fazer o bem, só pode acabar em... EM PIZZA!!!!!\n" +
                    "PIZZA SOLIDÁRIA!!!! Que maneira mais deliciosa de fazer o bem!Vamos ajudar a nossa Paróquia?\n" +
                    "A PIZZA SOLIDÁRIA é uma ação da Missão Guadalupe, em prol da Igreja da Vila Maceno com o apoio da @molecaggio. Garanta já o seu voucher(R$30, 00 cada), à venda na Casa de Missão ou na própria Igreja.\n" +
                    "Mussarela, Calabresa ou Presunto & Mussarela são os sabores que você pode escolher. om o voucher em mãos, a retirada da pizza pode ser feita em qualquer unidade Molecaggio Rio Preto, entre os dias 02.12.2020 e 31.01.2021, exceto às terças - feiras.\n" +
                    "“Quando um coração se preocupa com o outro, haverá sempre um milagre!” – Foi com essa frase tão linda que anunciamos esta ação tão especial. Aproveitamos a oportunidade para agradecer a todos os envolvidos!\n" +
                    "Arraste a imagem para o lado e saiba mais!", "Imagens/imagem17.jpg")
                        .AddPostedBy(missaoPerson)
                        .ModifyRegistrationDate(new DateTime(2020, 11, 29)));

                    if (!context.Notice.Any((q) => q.Id == noticeFive))
                        context.Notice.Add(new Domain.Notice(noticeFive, "Nossa Senhorinha de guadalupe", "O processo seletivo para a escolha da Nossa Senhorinha de Guadalupe e do Juan Dieguito vai começar!\n" +
                    "A primeira etapa que contará pontos será a foto mais curtida no Instagram.Sabemos que não será nada fácil escolher, mas contamos com a sua ajuda!São 16 participantes, e a dica é curtir a foto que mais tocou seu coração!\n" +
                    "As fotos serão postadas na sequência, fique de olho!\n" +
                    "O Projeto Guadalupinhos é uma inspiração do Padre Carlos e contará com 2 crianças, uma menina que representará a Nossa Senhorinha de Guadalupe e um menino que representará o Juan Dieguito, para serem os embaixadores mirins da Missão Guadalupe por um ano.\n " +
                    "Disse - lhe Jesus: Deixai vir a mim estas criancinhas e não as impeçais, porque o Reino dos Céus é para aqueles que se lhes assemelham” (Mt 19, 14).", "Imagens/imagem16.jpg")
                        .AddPostedBy(missaoPerson)
                        .ModifyRegistrationDate(new DateTime(2020, 11, 30)));

                    context.SaveChanges();
                }
            }
        }
    }
}
