using Guadalupe.Conexao.App.Model;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    public class LiturgyViewModel : ViewModel
    {
        public Liturgy Liturgy { get; private set; }
        public LiturgyViewModel(INavigation navigation, Liturgy liturgy) : base(navigation)
        {
            Liturgy = liturgy;
        }
    }
}
