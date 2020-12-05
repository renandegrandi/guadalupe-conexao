using Guadalupe.Conexao.App.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guadalupe.Conexao.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidateCodeView : ContentPage
    {
        public ValidateCodeView(string email)
        {
            InitializeComponent();
            BindingContext = new ValidateCodeViewModel(Navigation, App.UserRepository, App.SessionService, email);
        }
    }
}