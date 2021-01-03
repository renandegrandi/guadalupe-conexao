using Guadalupe.Conexao.App.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guadalupe.Conexao.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectsView : ContentPage
    {
        public ProjectsView()
        {
            InitializeComponent();
            BindingContext = new ProjectsViewModel(Navigation, App.ProjectRepository);
        }
    }
}