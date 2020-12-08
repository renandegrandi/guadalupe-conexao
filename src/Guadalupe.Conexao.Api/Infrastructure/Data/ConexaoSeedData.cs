using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

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

                    var missao = new Domain.Person("missaoguadalupe@hotmail.com", "Missão Guadalupe")
                        .AdicionarProfileImage("Imagens/profile.jpg");

                    var notices = new List<Domain.Notice>();

                    notices.Add(new Domain.Notice("O Bom Samaritano \nHomenagem aos 90 anos do nosso amado Padre Carlos", "Imagens/imagem21.jpg", missao)
                        .ModifyRegistrationDate(new DateTime(2020, 11, 27)));
                    notices.Add(new Domain.Notice("Mais uma semana abençoada está começando e hoje é dia de Grupo de Oração.\n" +
                    "Você não pode perder!A participação especial desta noite é da Mariana Faria, e a sua presença lá conosco é muito importante. Te esperamos, às 20h, na Igreja Maceno.As vagas são limitadas e por ordem de chegada.Antes, às 19h, tem Missa.\n" +
                    "Convide a família e os amigos. A transmissão ao vivo pelo YouTube também continua.", "Imagens/imagem19.jpg", missao)
                        .ModifyRegistrationDate(new DateTime(2020, 11, 09)));
                    notices.Add(new Domain.Notice("Terço da Misericórdia", "Imagens/imagem18.jpg", missao)
                        .ModifyRegistrationDate(new DateTime(2020, 11, 28)));
                    notices.Add(new Domain.Notice("Quando pessoas com o coração cheio de amor se reúnem para fazer o bem, só pode acabar em... EM PIZZA!!!!!\n" +
                    "PIZZA SOLIDÁRIA!!!! Que maneira mais deliciosa de fazer o bem!Vamos ajudar a nossa Paróquia?\n" +
                    "A PIZZA SOLIDÁRIA é uma ação da Missão Guadalupe, em prol da Igreja da Vila Maceno com o apoio da @molecaggio. Garanta já o seu voucher(R$30, 00 cada), à venda na Casa de Missão ou na própria Igreja.\n" +
                    "Mussarela, Calabresa ou Presunto & Mussarela são os sabores que você pode escolher. om o voucher em mãos, a retirada da pizza pode ser feita em qualquer unidade Molecaggio Rio Preto, entre os dias 02.12.2020 e 31.01.2021, exceto às terças - feiras.\n" +
                    "“Quando um coração se preocupa com o outro, haverá sempre um milagre!” – Foi com essa frase tão linda que anunciamos esta ação tão especial. Aproveitamos a oportunidade para agradecer a todos os envolvidos!\n" +
                    "Arraste a imagem para o lado e saiba mais!", "Imagens/imagem17.jpg", missao)
                        .ModifyRegistrationDate(new DateTime(2020, 11, 29)));
                    notices.Add(new Domain.Notice("O processo seletivo para a escolha da Nossa Senhorinha de Guadalupe e do Juan Dieguito vai começar!\n" +
                    "A primeira etapa que contará pontos será a foto mais curtida no Instagram.Sabemos que não será nada fácil escolher, mas contamos com a sua ajuda!São 16 participantes, e a dica é curtir a foto que mais tocou seu coração!\n" +
                    "As fotos serão postadas na sequência, fique de olho!\n" +
                    "O Projeto Guadalupinhos é uma inspiração do Padre Carlos e contará com 2 crianças, uma menina que representará a Nossa Senhorinha de Guadalupe e um menino que representará o Juan Dieguito, para serem os embaixadores mirins da Missão Guadalupe por um ano.\n " +
                    "Disse - lhe Jesus: Deixai vir a mim estas criancinhas e não as impeçais, porque o Reino dos Céus é para aqueles que se lhes assemelham” (Mt 19, 14).", "Imagens/imagem16.jpg", missao)
                        .ModifyRegistrationDate(new DateTime(2020, 11, 30)));
                    //notices.Add(new Domain.Notice("5ª mensagem teste - banco de dados.", "Imagens/imagem15.jpg", missao));
                    //notices.Add(new Domain.Notice("6ª mensagem teste - banco de dados.", "Imagens/imagem14.jpg", missao));
                    //notices.Add(new Domain.Notice("7ª mensagem teste - banco de dados.", "Imagens/imagem13.jpg", missao));
                    //notices.Add(new Domain.Notice("8ª mensagem teste - banco de dados.", "Imagens/imagem12.jpg", missao));
                    //notices.Add(new Domain.Notice("9ª mensagem teste - banco de dados.", "Imagens/imagem11.jpg", missao));
                    //notices.Add(new Domain.Notice("10ª mensagem teste - banco de dados.", "Imagens/imagem09.jpg", missao));
                    //notices.Add(new Domain.Notice("11ª mensagem teste - banco de dados.", "Imagens/imagem08.jpg", missao));
                    //notices.Add(new Domain.Notice("12ª mensagem teste - banco de dados.", "Imagens/imagem02.jpg", missao));

                    context.User.Add(renanUser);
                    context.Person.Add(missao);
                    context.Notice.AddRange(notices);

                    context.SaveChanges();
                }
            }
        }
    }
}
