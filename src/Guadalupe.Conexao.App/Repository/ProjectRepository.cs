using Guadalupe.Conexao.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Guadalupe.Conexao.App.Repository
{
    sealed class ProjectRepository : IProjectRepository
    {
        public readonly static IReadOnlyCollection<Project> _projects = new List<Project>
        {
            new Project { 
                Id = Guid.NewGuid(),
                Name = "Capelinha",
                Description = @"<p>Este projeto pretende divulgar o terço e a devoção a Nossa Senhora de Guadalupe, seja nas famílias, nas empresas ou nas comunidades, sobretudo onde impera o sofrimento, por toda e qualquer causa. Consiste em:</p>
<ul>
    <li>Levar a capelinha a uma família que a pede ou aceita-a, a pedido da equipe de devotos comprometidos;</li>
    <li>Ali, reza-se o “TERÇO DA BENÇÃO”, um conjunto de preces dirigidas à Senhora de Guadalupe.A respeito desta oração, nunca nos esqueçamos das palavras da santa vidente de Fátima, Irmã Lucia, que diz ”Não há graça que não se obtenha com a reza do terço”! O Papa Francisco manda: “Nunca deixem de rezar o rosário! Minha mãe me ensinou desde criança e hoje rezo o rosário! Quantas graças”!;</li>
    <li>A Missão providenciará, através da mãe deste projeto, a capelinha e todas as orações, com os mistérios, orações de devoção e as especiais;</li>
    <li>Cabe à equipe determinar o cronograma das visitas, ou seja, dia, hora e local, sempre com muito amor, pois a Virgem promete-lhes, como ao seu primeiro mensageiro, São Juan Diego: “Eu os farei muito felizes em recompensa das fadigas que tiverem para me servir”.</li>
</ul>",
                Image = "resource://Guadalupe.Conexao.App.Resource.Image.projeto_capelinha_guadalupe_300x300.webp",
                Logo ="resource://Guadalupe.Conexao.App.Resource.Image.projeto_capelinha_guadalupe_without_border_300x300.webp",
                Contact = "5517991328424"
                 
            },
            new Project 
            {
                Id = Guid.NewGuid(),
                Name = "Colo de Maria",
                Description = @"projeto colo de maria
                    O projeto consiste em amparar nascituros indesejados em risco e as respectivas mães, proporcionando condições favoráveis às vidas deles. Uma forma bastante interessante de realização deste projeto vem sendo o envolvimento de várias “mães” beneficentes. Elas desenvolvem sabedoria e muito amor para preservar e promover a vida dos nascituros, dos recém-nascidos e das respectivas mães em risco de perdê-la. O projeto recomenda visitas à essas grávidas ou ainda à essas mamães, afim de cuidar e zelar pela vida. Parcerias com o poder público costumam ajudar bastante e se faz essencial a orientação da Missão Guadalupe no direcionamento de cada caso, uma vez que cuidamos especificamente de cada caso em particular, por ser único e distinto dos demais. O importante aqui é querer e começar, a experiência ensinará os melhores caminhos. AQUI, A VIDA VALE MAIS!.
                    Venha conosco dar um colo para aqueles que, muitas vezes, ainda nem nasceram e sim, merecem uma chance!
                    Vem ...
                    Mãe do projeto",
                Image = "resource://Guadalupe.Conexao.App.Resource.Image.projeto_colo_de_maria_300x300.webp",
                Logo ="resource://Guadalupe.Conexao.App.Resource.Image.projeto_colo_de_maria_without_border_300x300.webp",
                Contact = "5517996582983"
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "Abraço de Mãe",
                Description = @"Projeto muito parecido a “Casas de Acolhida”, mas para as nossas crianças e jovens desamparados também, de 0 a 18 anos de idade, onde cuidamos, tratamos, ressignificamos e restauramos essas vidas através do amor, atenção e carinho, que somente nossa mãezinha do céu pode conceder. Sediado em Cacoal – RO, é mais uma obra que necessita e merece muito amor, ajuda e carinho por parte da comunidade e, sobretudo, dos amigos e benfeitores da Missão.
                    Venha conosco levar esperança para aqueles que talvez o mundo tenha esquecido. Nesse caso, vidas tão jovens!",
                Image = "resource://Guadalupe.Conexao.App.Resource.Image.abraco_de_mae_300x300.webp",
                Logo ="resource://Guadalupe.Conexao.App.Resource.Image.abraco_de_mae_without_border_300x300.webp",
                Contact = "5569992073056"
            },
            //new Project
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Casas de Acolhida",
            //    Description = @"Este projeto tem por objetivo estabelecer e sustentar casas de acolhida para pessoas adultas, as mais desamparadas da rua, sobretudo os anciãos e andarilhos sem família. Já está em funcionamento uma delas, fundada pelo casal Santa Selma e José Carlos no ano de 2003, na cidade de Cacoal – RO. Abriga cerca de 100 carentes, prestando-lhes a mais carinhosa assistência sócio-caritativa, na estimativa das entidades de saúde do estado.
            //        Este casal, em conhecimento dos Mensageiros de Nossa Senhora de Guadalupe, na qualidade de pessoas “consagradas” e fundadoras da Missão Guadalupe, ficou de tal forma atraído, que quis, de imediato, iniciar a formação para se juntarem a eles, como consagrados e aliados nas obras da Missão. Tal consagração se deu após um ano de preparação, pelo Natal do ano de 2018, no mês de dezembro. A união das duas entidades foi selada juridicamente em 2019, por meio de um documento aceito e assinado por ambas as partes. Nele, declara-se que a Casa de Acolhida São Camilo passa a ser administrada pela Associação Mensageiros de Guadalupe, entidade jurídica a serviço do “Instituto Missionário dos Mensageiros de Nossa Senhora de Guadalupe”. Na ocasião, também foi anexada, a obra do projeto “Abraço de Mãe”, apenas iniciada pelo mesmo casal.
            //        Esse é mais um projeto da Missão Guadalupe que visa acolher, dar atenção carinho, amor e alegria às pessoas menos favorecidas e sem famílias, sobretudo andarilhos e afins. Venha conosco acolher e cuidar desses que tanto necessitam!
            //    ",
            //    Image = "resource://Guadalupe.Conexao.App.Resource.Image.abraco_de_mae_300x300.webp",
            //    Logo ="resource://Guadalupe.Conexao.App.Resource.Image.abraco_de_mae_without_border_300x300.webp"
            //},
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "Jovens em Missão",
                Description = @"É a força jovem na Missão Guadalupe. Este projeto tem por objetivo cuidar dos nossos jovens, às vezes mal entendidos pela sociedade ou ainda pela família. Composto por adolescentes e jovens, campistas ou não, o projeto propõem a divulgação de absolutamente tudo dessa Senhora, da Sua missão e da Missão Guadalupe. Tais cuidados vão muito além da tão somente divulgação e com este intuito acaba por evangelizar, formar, catequisar e trazêlos de volta para Nosso Senhor Jesus Cristo e à Igreja. Trazendo-os para os ensinamentos da palavra de Deus, acaba por desenvolver atividades do universo jovem católico, afastando, assim, dos vícios das drogas, bebidas, cigarros e vida desregrada. Os acampamentos FAC (Formação do Adolescente Católico) e Acampamento JUVENIL são bons exemplos de conduta onde os jovens se unem em preparação e/ou formação propriamente dita, para cuidar de tantos outros, por ora perdidos, esquecidos ou ainda simplesmente distantes. Outro bom exemplo é o grupo de oração para os jovens no sábado ao final da tarde. Muito embora a data e horário pareçam pouco lógicos, é, de fato, proposital, a fim de que esses jovens se organizem para o final de semana, muitas vezes porta de entrada para os males do mundo.
                    Como dizia São Francisco de Assis, se Deus é por nós, quem será contra nós? Vamos juntos viver intensamente tudo aquilo que Deus tem nos preparado!",
                Image = "resource://Guadalupe.Conexao.App.Resource.Image.jovens_em_missão_300x300.webp",
                Logo ="resource://Guadalupe.Conexao.App.Resource.Image.jovens_em_missão_without_border_300x300.webp",
                Contact = "5517991089924"
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "Missão Guadalupe Orante",
                Description = @"Para o bom sucesso da Missão Guadalupe, consideramos essencial projeto da oração constante, de dia e de noite, 24 horas por dia, 7 dias por semana, a fim de agradecer, louvar, adorar e pedir por ela. Para esse efeito, estamos organizando um grupo muito grande de pessoas, repartido em 48 grupos, cada qual se comprometendo em oferecer até 30 minutos de oração pela Missão, em horário pré-fixado e permanente. E as pessoas que precisam trabalhar durante o dia, como é que podem rezar ao mesmo tempo? Assim é, aprendem a rezar e a trabalhar ao mesmo tempo, com as mãos, trabalham e com a boca ou em pensamentos e com o coração, rezam. Aliás, é um ótimo exercício para a grande virtude da união com Deus! Distrações? Fazem parte, e assim vamos melhorando ...
                    É necessário que surjam intercessores para tapar as brechas do que são os pecados e as fraquezas do nosso povo. Não são os grupos, as comunidades que clamam por intercessores, mas é Deus quem os procura, ansiosamente. É ele quem os quer, quem os deseja.
                    A Missão Guadalupe Orante quer formar um grande exército de homens e mulheres orantes, que preencham todas as brechas. Pessoas comprometidas em orar pela Missão, pelos projetos, pela família, pelos doentes, pelos necessitados e por todos que pedem oração.
                    Ações dos intercessores:
                    - Terços ao vivo pelas redes sociais;
                    - Grupos de intercessores que oram de manhã, tarde e noite;
                    - Grupos de intercessores que oram nas residências e empresas;
                    - Contínua formação de intercessores.
                    Aliste-se neste exército, sabendo ou não rezar.
                    DEUS NÃO ESCOLHE OS CAPACITADOS, ELE CAPACITA OS ESCOLHIDOS",
                Image = "resource://Guadalupe.Conexao.App.Resource.Image.missao_guadalupe_orante_300x300.webp",
                Logo ="resource://Guadalupe.Conexao.App.Resource.Image.missao_guadalupe_orante_without_border_300x300.webp",
                Contact = "5517997781004"
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "Pequeninos da Missão",
                Description = @"Assim como o próprio Jesus intercedeu por nós, nós também podemos estar entre Deus e uma causa!
                    Consegue imaginar a grandeza que é isto?
                    Nós, da Missão Guadalupe, convidamos você a participar do nosso Ministério de Intercessão.
                    Com o amor de Nossa Senhora de Guadalupe, participe dessa experiência, venha rezar conosco!",
                Image = "resource://Guadalupe.Conexao.App.Resource.Image.pequeninos_da_missao_300x300.webp",
                Logo ="resource://Guadalupe.Conexao.App.Resource.Image.pequeninos_da_missao_without_border_300x300.webp",
                Contact = "5517996761981"
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "Dependentes Químicos",
                Description = @"O projeto consiste em realizar alguns tipos de ações com os dependentes químicos e/ou alcoólicos, conforme abaixo:
                    Prevenção – Realiza ações a fim de desestimular o vício, através de palestras, vídeos educativos, testemunhos, grupos de oração e de ajuda mútua, dentre outros;
                    Recuperação – Providencia a internação em clínicas de recuperação especializadas, oferece vagas nos acampamentos da Missão, a fim de, dentre outros, promover a ressocialização daqueles nesta fase específica, faz o acompanhamento em todas as fases, seja inicial (encaminhamento às clínicas e adaptação) e ou final (após a saída das clínicas até a ressocialização, de falto) e ainda propicia e ajuda na busca de um emprego, residência, etc. Ainda nos processos de recuperação, a Missão Guadalupe se incumbe de levar a palavra de Deus incessantemente aos em recuperação, em várias clínicas próximas a nossa cidade, firmando assim os planos de Nosso Senhor para essas vidas tão afetadas;
                    O projeto ainda combina com a “Pastoral da Sobriedade” e é, às vezes, trabalhado em parceria com esta;
                    Muitos dos recuperados, ainda, por gratidão a Deus e também aqueles que os ajudaram, bem como por compaixão aos “ex-companheiros”, ajudam neste projeto em suas comunidades.

                    É com esse pequeno trecho do evangelho de São Marcos que os convidamos para participar desse nosso lindo projeto! Talvez você pense mas eu não sei pregar, não sei evangelizar, não sei falar em público”... Deus nos pede apenas um coração disponível, portanto, se você pode, venha conosco levar um abraço, fazer uma oração, tocar algum instrumento ou até mesmo um simples olhar de carinho a esses irmãos que tanto precisam!
                    Vamos juntos levar esse amor de Deus e cada vez mais evangelizar como Jesus fazia:
                    Com boas ações, afinal palavras convencem, mas o exemplo arrasta!",
                Image = "resource://Guadalupe.Conexao.App.Resource.Image.post_dependentes_quimicos_300x300.webp",
                Logo ="resource://Guadalupe.Conexao.App.Resource.Image.post_dependentes_quimicos_without_border_300x300.webp",
                Contact = "5517991465831"
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "Intercessão",
                Description = @"Assim como o próprio Jesus intercedeu por nós, nós também podemos estar entre Deus e uma causa!
                    Consegue imaginar a grandeza que é isto?
                    Nós, da Missão Guadalupe, convidamos você a participar do nosso Ministério de Intercessão.
                    Com o amor de Nossa Senhora de Guadalupe, participe dessa experiência, venha rezar conosco!",
                Image = "resource://Guadalupe.Conexao.App.Resource.Image.projeto_intercessao_300x300.webp",
                Logo ="resource://Guadalupe.Conexao.App.Resource.Image.projeto_intercessao_without_border_300x300.webp",
                Contact = "5517997018368"
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "Pessoas em Situação de Rua",
                Description = @"Estamos falando, aqui, de um projeto muito abrangente, cujo objetivo essencial é chegar à inclusão dessas pessoas nas suas famílias, na sociedade e na comunidade religiosa. A Missão Guadalupe cuida dessas pessoas em algumas modalidades, como segue abaixo:

                    - Promove ações pontuais para levar alegria, amor e muito carinho a esses pequeninos, como o já consolidado domingo de Páscoa, onde a Missão realiza um dia inteiro dedicado a eles, com muita música, brincadeiras e comida de qualidade, cuida dos cabelos e barba, faz doações de roupas, abrigos, além de sorteios de brindes e tantas outros, que só presenciando é que se consegue ter alguma dimensão do que é feito;

                    - Realiza ações que visam cuidar especificamente de cada um, de cada problema individual, de cada dor, promovendo sempre o perdão, sobretudo aos familiares, levando ao pé da letra a passagem bíblica: “21. Então Pedro aproximou-se de Jesus e perguntou: Senhor, quantas vezes deverei perdoar a meu irmão quando ele pecar contra mim? Até sete vezes? 22.Jesus respondeu: 'Eu digo a você: Não até sete, mas até setenta vezes sete” (Mt 18, 21-22).

                    - Encaminha ao projeto “Dependentes Químicos” da própria Missão Guadalupe, aqueles que se encontram em qualquer situação de dependência, seja ela química, alcoólica ou qualquer outra e que estejam abertos a essa mudança de vida, pensamento e caráter (metanóia). Ali, tentamos o encaminhamento às clínicas especializadas parceiras.
                    Este projeto consiste em encontrar maneiras de amenizar o sofrimento de pessoas nessa situação. Fazemos também a reintegração social e familiar, com a ajuda de outras entidades parceiras, realizamos outros projetos, tais como os acampamentos, tratamentos de dependências, grupo de oração, reuniões específicas para partilhas, etc.",
                Image = "resource://Guadalupe.Conexao.App.Resource.Image.projeto_morador_de_rua_novo_300x300.webp",
                Logo ="resource://Guadalupe.Conexao.App.Resource.Image.projeto_morador_de_rua_novo_without_border_300x300.webp",
                Contact = "5517997772681"
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "Bartimeu",
                Description = @"Cada vez mais, cresce o número de idosos e doentes solitários em nosso país e a solidão, do ponto de vista psicológico, é considerada “um aglomerado complexo de sentimentos, composto de raiva, medo ansiedade, tristeza e vergonha, que acaba por trazer muito sofrimento”. E o pior, essas pessoas acabam por padecer, já que possuem 50% mais de chances de morrer prematuramente em comparação a seus contemporâneos não-solitários.
                    O projeto refere-se ao cuidado que se deve prestar aos idosos e aos doentes mais solitários, que gritam por socorro, como o pobre e cego Bartimeu (último milagre de Jesus):
                    “Filho de Davi, tem piedade de mim”! Dessa forma, esse projeto conta com uma equipe comunitária, integrada por pessoas com disponibilidade, interesse, vocação ou ainda um chamado para visitar, alegrar e ajudar, de alguma forma essas pessoas tão necessitadas.
                    Esse é mais um projeto da Missão Guadalupe que visa dar atenção carinho, amor e alegria às pessoas nos asilos, lares especializados e hospitais. Venha conosco fazer sorrir e dar amor a quem muito já nos fez!",
                Image = "resource://Guadalupe.Conexao.App.Resource.Image.projeto_os_bartimeus_300x300.webp",
                Logo ="resource://Guadalupe.Conexao.App.Resource.Image.projeto_os_bartimeus_without_border_300x300.webp",
                Contact = "5517992195151"
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "Saúde e Vida",
                Description = @"Em nosso país, tão necessitado de assistência sanitária, os nossos bispos (CNBB), em 09 de maio de 1980, instituíram oficialmente a PASTORAL DA SAÚDE e recomendaram instantemente a implantação da mesma. Pela importância que tem, em referência a seus objetivos, não pode ser negligenciada. Assim, quando uma comunidade descuida da saúde e da vida dos seus membros necessitados, a mesma perde a sua identidade cristã. Nos casos onde o Estado falha em cumprir com sua obrigação de promover esse direito básico do cidadão, a MISSÃO GUADALUPE, por meio deste projeto, pretende ir de encontro a essa importante instituição da Igreja e, por isso, propõe-se:
                    - Ministrar Cursos;
                    - Colaborar com as equipes diocesanas na formação de lideranças da saúde;
                    - Divulgar as cartilhas da saúde;
                    - Capacitar pessoas e promover ações para tanto, entre outros.
                    Saúde não é só a ausência de doença, mas um completo bem estar físico, mental, social e, sobretudo espiritual! Vem fazer parte desse time! A partir de pequenas ações podemos transformar vidas",
                Image = "resource://Guadalupe.Conexao.App.Resource.Image.projeto_saude_e_vida_300x300.webp",
                Logo ="resource://Guadalupe.Conexao.App.Resource.Image.projeto_saude_e_vida_without_border_300x300.webp",
                Contact = "5517997134352"
            },
            //new Project
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Vicentinos",
            //    Description = @"Este projeto consiste em organizar conferências vicentinas nas comunidades onde são necessárias ou revitalizá-las nos locais onde já existem e são “fracas”, ou seja, fortalecer onde já existem, criar e dar suporte onde ainda não existem. Nos parece bastante razoável buscar conhecimento onde os vicentinos atuam de forma estruturada e eficaz, difundir os acertos, bem como prevenir quanto aos erros. Dessa forma, toda a comunidade vicentina ganha, assim podemos fazer do serviço aos pobres, nossos Mestres e Senhores, fazendo com que os vicentinos sejam um só rosto e uma só força a viver o amor e a caridade. Os pobres, nós Os enxergamos com os olhos da carne. Eles estão aí, podemos tocar o dedo em suas chagas. Aqui a incredulidade não é mais possível, é preciso cair de joelhos aos seus pés e gritar: MEU SENHOR E MEU DEUS! Vocês são nossos Mestres e seremos Seus servos. Vocês são para nós imagens sagradas do Deus que não enxergamos. Não podendo amá-Lo de outro jeito, nós O amaremos em vocês. Aqueles que não abrem mão de viver o Carisma Vicentino serão sempre pessoas livres, desapegadas, renovadas, rejuvenescidas e inseridas. Pessoas que por onde passam transbordam alegria, vida e fraternidade.
            //        Saúde não é só a ausência de doença, mas um completo bem estar físico, mental, social e, sobretudo, espiritual! Vem fazer parte desse time! A partir de pequenas ações podemos transformar vidas!",
            //    Image = "resource://Guadalupe.Conexao.App.Resource.Image.abraco_de_mae_300x300.webp",
            //    Logo ="resource://Guadalupe.Conexao.App.Resource.Image.abraco_de_mae_without_border_300x300.webp"
            //},
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "S.O.S. Familia",
                Description = @"Tem entre seus objetivos, visitar as famílias mais carentes a fim de identificar quais as maiores necessidades e buscar ajuda, e, quando necessário, encaminhar a alguma pastoral ou projeto específico. Além do mais importante, rezar junto com estas famílias, promovendo entre eles a oração e a busca pelo Senhor.",
                Image = "resource://Guadalupe.Conexao.App.Resource.Image.sos_familias_300x300.webp",
                Logo ="resource://Guadalupe.Conexao.App.Resource.Image.sos_familias_without_border_300x300.webp",
                Contact = "5517997440365"
            }
        };

        public Project Get(Guid id)
        {
            return _projects.FirstOrDefault((p) => p.Id == id);
        }

        public List<Project> Get()
        {
            return _projects.ToList();
        }
    }
}
