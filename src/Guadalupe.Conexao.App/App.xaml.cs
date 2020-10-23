using Guadalupe.Conexao.App.Extensions;
using Guadalupe.Conexao.App.Repository;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new View.MainView());
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
