using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    public class LiturgyViewModel : ViewModel
    {
        private const int SecondReading = 1;

        public Liturgy Liturgy { get; private set; }
        private bool HideSecondReading
        {
            get
            {
                return (Liturgy == null || Liturgy.SecondReading == null);
            }
        }
        public ReadingViewModel FirstReadingViewModel { get; private set; }
        public ReadingViewModel SecondReadingViewModel { get; private set; }
        public ReadingViewModel PsalmViewModel { get; private set; }
        public ReadingViewModel GospelViewModel { get; private set; }
        public LiturgyViewModel(INavigation navigation, TabbedPage page) : base(navigation)
        {
            //Simulando uma chamada ao servidor.
            Task.Run(() =>
            {
                Liturgy = new Liturgy
                {
                    FirstReading = new Reading(),
                    SecondReading = null,
                    Gospel = new Reading(),
                    Psalm = new Reading()
                };

                hideSecondReadingIfNeeded(page);

                FirstReadingViewModel = new ReadingViewModel(navigation, Liturgy.FirstReading);
                SecondReadingViewModel = new ReadingViewModel(navigation, Liturgy.SecondReading);
                PsalmViewModel = new ReadingViewModel(navigation, Liturgy.Psalm);
                GospelViewModel = new ReadingViewModel(navigation, Liturgy.Gospel);

            }).ConfigureAwait(false);
        }
        private void hideSecondReadingIfNeeded(TabbedPage page) 
        {
            if (!HideSecondReading)
                page.Children.RemoveAt(SecondReading);
        }
    }
}
