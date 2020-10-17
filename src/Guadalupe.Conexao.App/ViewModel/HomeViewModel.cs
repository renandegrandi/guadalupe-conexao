using Guadalupe.Conexao.App.Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    public class HomeViewModel : ViewModel
    {
        #region Propriedades

        public IReadOnlyCollection<Notice> News { get; set; }

        #endregion

        #region Construtores
        public HomeViewModel(INavigation navigation) : base(navigation)
        {
            var missaoGuadalupe = new User
            {
                Id = Guid.NewGuid(),
                Image = "profile.jpg",
                Name = "Missão Guadalupe"
            };

            News = new List<Notice>
            {
                new Notice {
                    Id = Guid.NewGuid(),
                    Message = @"🤩 Bom dia com alegria e muito amor no coração, porque hoje é Dia das Crianças, é Dia de Nossa Senhora Aparecida e é dia de GRUPO DE ORAÇÃO!!!" +
                        "🙏🏻Nem precisamos dizer que vai ser lindo, né? A convidada dessa noite é a Lidy Souza. Ela vai falar sobre a “Vida Missionária de Maria”, e está preparando tudo com muito carinho. " +
                        "🙌🏻Esperamos você, às 20h, na Igreja Maceno. O número de pessoas permitido é limitado e será por ordem de chegada. " +
                        "😉Convide a família e os amigos. A transmissão ao vivo pelo YouTube também continua. " +
                        "🌐Link: https://bit.ly/3gHcMzL (inscreva-se no nosso canal do YouTube. Link direto na bio.)",
                    Image = "notice.jpg",
                    PostedBy = missaoGuadalupe
                },                
                new Notice {
                    Id = Guid.NewGuid(),
                    Message = "😴 Soninho dos pequenos protegido e abençoado com a Naninha da Nossa Senhora de Guadalupe.",
                    Image = "notice.jpg",
                    PostedBy = missaoGuadalupe
                },

            };
        }
        #endregion


    }
}
