using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Service;
using Guadalupe.Conexao.App.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    public class ProfileNavViewModel : ViewModel
    {

        #region Properties

        public Person Person { get; private set; }

        #endregion

        #region Constructor

        public ProfileNavViewModel(INavigation navigation, ISessionService sessionService) : base(navigation)
        {
            Person = sessionService.GetUser().Person;
        }

        #endregion

        #region Commands

        public Command OnNavigateToHome => new Command(() =>
        {
            var masterDetail = Application.Current.MainPage as MasterDetailPage;
            masterDetail.IsPresented = false;

            masterDetail.Detail = new NavigationPage(new HomeView());

            ((NavigationPage)masterDetail.Detail).BarBackgroundColor = Color.Tomato;

        });

        public Command OnNavigateToProject => new Command(() =>
        {
            var masterDetail = Application.Current.MainPage as MasterDetailPage;
            masterDetail.IsPresented = false;
            masterDetail.Detail = new NavigationPage(new ProjectsView());

            ((NavigationPage)masterDetail.Detail).BarBackgroundColor = Color.Tomato;
        });

        #endregion
    }
}
