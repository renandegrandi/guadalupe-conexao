using Guadalupe.Conexao.App.Extensions;
using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository;
using Guadalupe.Conexao.App.Repository.DTO;
using Guadalupe.Conexao.App.Validation;
using Guadalupe.Conexao.App.View;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    sealed class AutenticationViewModel: FormViewModel, IDisposable
    {
        #region Dependencies

        private readonly CancellationToken _cancellationToken;

        #endregion

        #region Constructor

        public AutenticationViewModel(INavigation navigation) : base(navigation)
        {
            CrossFacebookClient.Current.OnUserData += FacebookOnUserData;
            _cancellationToken = new CancellationToken();

            Email = new ValidatableObject<string>(new EmailRule("O Email está invalido!"));   
        }

        #endregion

        public bool FormValid
        {
            get
            {
                return Email.Validate();
            }
        }
        public ValidatableObject<string> Email { get; set; }
        public string MessageError { get; set; }
        public ICommand OnLoginFacebookCommand => new Command(async () =>
        {
            var requestFields = ((Facebook.Perfil[])Enum.GetValues(typeof(Facebook.Perfil)))
             .Select((perfil) => perfil.GetDescription());

            var permisions = new string [] { };

            await CrossFacebookClient.Current.RequestUserDataAsync(requestFields.ToArray(), permisions);
        });
        public ICommand OnLoginEmailCommand => new Command(async () =>
        {
            await ConexaoHttpClient.SendNewCodeByEmailAsync(this.Email.Value, this._cancellationToken);

            await _navigation.PushAsync(new ValidateCodeView(this.Email.Value));

        }, () => {
            return Email.IsValid;
        });
            
        private readonly EventHandler<FBEventArgs<string>> FacebookOnUserData = (object sender, FBEventArgs<string> e) =>
        {
            if (e == null) return;

            switch (e.Status)
            {
                case FacebookActionStatus.Completed:

                    var facebookDto = JsonConvert.DeserializeObject<FacebookDto>(e.Data);

                    break;
                case FacebookActionStatus.Canceled:
                    break;
            }
        };

        public void Dispose()
        {
            //TODO: Validar se o dispose está sendo chamado.
            CrossFacebookClient.Current.OnUserData -= FacebookOnUserData;

            this._cancellationToken.ThrowIfCancellationRequested();
        }

        ~AutenticationViewModel() 
        {
            Dispose();
        }
    }
}
