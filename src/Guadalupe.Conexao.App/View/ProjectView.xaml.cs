using Guadalupe.Conexao.App.Service;
using Guadalupe.Conexao.App.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guadalupe.Conexao.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectView : ContentPage
    {
        public ProjectView(Guid project)
        {
            InitializeComponent();
            BindingContext = new ProjectViewModel(Navigation, App.ProjectRepository, new WhatsappService(), project);
        }

    }
}