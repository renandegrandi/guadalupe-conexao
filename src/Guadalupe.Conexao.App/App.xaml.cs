using Guadalupe.Conexao.App.Repository;
using Guadalupe.Conexao.App.Service;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App
{
    public partial class App : Application
    {
        public static readonly IUserRepository UserRepository = new UserRepository();
        public static readonly INoticeRepository NoticeRepository = new NoticeRepository();
        public static readonly IProjectRepository ProjectRepository = new ProjectRepository();
        public static readonly ISessionService SessionService = new SessionService(UserRepository);

        public App()
        {
            InitializeComponent();

            if (SessionService.IsAutenticated)
            {
                MainPage = new NavigationPage(new View.MainView());
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
