using Guadalupe.Conexao.App.Repository;
using Guadalupe.Conexao.App.Service;
using Guadalupe.Conexao.App.View;
using Guadalupe.Conexao.App.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guadalupe.Conexao.App
{
    public partial class App : Application
    {
        public static readonly IUserRepository UserRepository = new UserRepository();
        public static readonly INoticeRepository NoticeRepository = new NoticeRepository();
        public static readonly IProjectRepository ProjectRepository = new ProjectRepository();
        public static readonly ISessionService SessionService = new SessionService(UserRepository);
        public static readonly IPopupService PopupService = new PopupService();

        public App(Initialize init = null)
        {
            InitializeComponent();

            if (SessionService.IsAutenticated)
            {
                MainPage = new NavigationPage(new View.MainView());

                if (init != null) 
                {
                    switch (init.Page)
                    {
                        case Initialize.Pages.Notice:
                            MainPage.Navigation.PushModalAsync(new NoticeView(init.IdRegister))
                                .SafeFireAndForget(false);
                            break;
                        case Initialize.Pages.Project:
                            MainPage.Navigation.PushModalAsync(new ProjectView(init.IdRegister))
                                .SafeFireAndForget(false);
                            break;
                        default:
                            break;
                    }   
                }
            }
            else 
            {
                MainPage = new NavigationPage(new View.AutenticationView());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
