using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guadalupe.Conexao.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LiturgyView : TabbedPage
    {
        public LiturgyView(Liturgy liturgy)
        {
            InitializeComponent();

            BindingContext = new LiturgyViewModel(Navigation, liturgy);

            var firstReadingPage = new ReadingView(liturgy.FirstReading)
            {
                Title = "1ª Leitura"
            };

            Children.Add(firstReadingPage);

            if (liturgy.SecondReading != null) 
            {
                var secondRedingPage = new ReadingView(liturgy.SecondReading)
                {
                    Title = "2ª Leitura"
                };
                Children.Add(secondRedingPage);
            }

            var psalmPage = new ReadingView(liturgy.Psalm)
            {
                Title = "Salmo"
            };

            Children.Add(psalmPage);

            var gospelPage = new ReadingView(liturgy.Gospel) 
            {
                Title = "Evangelho"
            };

            Children.Add(gospelPage);
        }
    }
}