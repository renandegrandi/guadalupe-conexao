using Guadalupe.Conexao.App.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guadalupe.Conexao.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoticeView : ContentPage
    {
        public NoticeView(Guid notice)
        {
            InitializeComponent();
            BindingContext = new NoticeViewModel(Navigation, App.NoticeRepository, notice);
        }
    }
}