using Guadalupe.Conexao.App.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guadalupe.Conexao.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutenticationView : ContentPage
    {
        public AutenticationView()
        {
            InitializeComponent();
            BindingContext = new AutenticationViewModel(Navigation);
        }
    }
}