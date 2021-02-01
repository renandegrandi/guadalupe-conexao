using Guadalupe.Conexao.App.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guadalupe.Conexao.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileNavView : ContentPage
    {
        public ProfileNavView()
        {
            InitializeComponent();
            BindingContext = new ProfileNavViewModel(Navigation, App.SessionService);
        }
    }
}