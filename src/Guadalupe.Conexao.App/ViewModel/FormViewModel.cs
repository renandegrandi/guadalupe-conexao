using Guadalupe.Conexao.App.Service;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    abstract class FormViewModel: ViewModel
    {
        protected FormViewModel(INavigation navigation) : base(navigation) { }

        protected FormViewModel(INavigation navigation, IPopupService popupService): base(navigation, popupService)
        {

        }
    }
}
