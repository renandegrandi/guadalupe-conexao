using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Service;
using Guadalupe.Conexao.App.View;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    public class ProfileNavViewModel : ViewModel
    {
        public Person Person { get; private set; }

        public ProfileNavViewModel(INavigation navigation, ISessionService sessionService) : base(navigation)
        {
            Person = sessionService.GetUser().Person;
        }

        public Command OnNavigateToHomeCommand => new Command(() =>
        {
            var masterDetail = Application.Current.MainPage as MasterDetailPage;
            masterDetail.IsPresented = false;

            masterDetail.Detail = new NavigationPage(new HomeView());

            ((NavigationPage)masterDetail.Detail).BarBackgroundColor = Color.Tomato;

        });

        public Command OnNavigateToProjectCommand => new Command(() =>
        {
            var masterDetail = Application.Current.MainPage as MasterDetailPage;
            masterDetail.IsPresented = false;
            masterDetail.Detail = new NavigationPage(new ProjectsView());

            ((NavigationPage)masterDetail.Detail).BarBackgroundColor = Color.Tomato;
        });

        public Command OnNavigateToLiturgyCommand => new Command(async () =>
        {
            var masterDetail = Application.Current.MainPage as MasterDetailPage;
            masterDetail.IsPresented = false;

            await Task.Run(() =>
            {
                var Liturgy = new Liturgy
                {
                    FirstReading = new Reading().SetMock(),
                    SecondReading = null,
                    Gospel = new Reading().SetMock(),
                    Psalm = new Reading().SetMock()
                };

                masterDetail.Detail = new NavigationPage(new LiturgyView(Liturgy));

                ((NavigationPage)masterDetail.Detail).BarBackgroundColor = Color.Tomato;
            });
        });

    }
}
