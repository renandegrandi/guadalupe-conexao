using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace Guadalupe.Conexao.App.Service
{
    sealed class WhatsappService : IWhatsappService
    {
        private const string Url = "whatsapp://send?phone={0}";

        public Task OpenAsync(string phone, string message)
        {
            try
            {
                var url = Url.Replace("{0}", phone);

                if (!string.IsNullOrWhiteSpace(message))
                    url += "&text=" + message;

                return Launcher.OpenAsync(new Uri(url));
            }
            catch (Exception ex)
            {
                Log.Warning(nameof(WhatsappService), ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
