using Guadalupe.Conexao.App.Model;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    public class ReadingViewModel: ViewModel
    {
        public Reading Reading { get; private set; }

        public ReadingViewModel(INavigation navigation, Reading reading) : base(navigation)
        {
            Reading = reading;
        }
    }
}
