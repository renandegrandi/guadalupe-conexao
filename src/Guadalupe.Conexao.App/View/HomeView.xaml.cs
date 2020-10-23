using Guadalupe.Conexao.App.Repository;
using Guadalupe.Conexao.App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guadalupe.Conexao.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(Navigation, new NoticeRepository());
        }
    }
}