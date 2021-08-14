using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guadalupe.Conexao.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadingView : ContentPage
    {
        public ReadingView(Reading reading)
        {
            InitializeComponent();

            BindingContext = new ReadingViewModel(Navigation, reading);
        }
    }
}