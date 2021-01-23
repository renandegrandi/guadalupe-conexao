using Guadalupe.Conexao.App.Extensions;
using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository;
using Guadalupe.Conexao.App.Repository.DTO;
using Guadalupe.Conexao.App.Service;
using Guadalupe.Conexao.App.Validation;
using Guadalupe.Conexao.App.View;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Guadalupe.Conexao.App.ViewModel
{
    sealed class AutenticationViewModel: FormViewModel, IDisposable
    {
        #region Fields

        private bool _isLoading;

        #endregion

        #region Constructor

        public AutenticationViewModel(INavigation navigation, IPopupService popupService) : base(navigation, popupService)
        {
            CrossFacebookClient.Current.OnUserData += FacebookOnUserData;

            Email = new ValidatableObject<string>(new EmailRule("O Email está invalido!"));   
        }

        #endregion

        #region Properties

        public bool FormValid
        {
            get
            {
                return Email.Validate();
            }
        }
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public ValidatableObject<string> Email { get; set; }

        #endregion

        #region Commands

        public ICommand OnLoginFacebookCommand => new Command(async () =>
        {
            var requestFields = ((Facebook.Perfil[])Enum.GetValues(typeof(Facebook.Perfil)))
             .Select((perfil) => perfil.GetDescription());

            var permisions = new string[] { };

            await CrossFacebookClient.Current.RequestUserDataAsync(requestFields.ToArray(), permisions);
        });
        public ICommand OnLoginEmailCommand => new Command(async () =>
        {
            try
            {

                IsLoading = true;

                await ConexaoHttpClient.SendNewCodeByEmailAsync(this.Email.Value, this._cancellationToken);

                await _popupService.ShowAsync("Código de acesso", $"Enviamos o código de acesso para: {this.Email.Value}, consulte sua caixa de e-mail, caso não encontrar verifique os spans!", "Continuar");

                await _navigation.PushAsync(new ValidateCodeView(this.Email.Value));
            }
            catch (Exception ex)
            {
                Log.Warning("Exception", ex.Message);

                await _popupService.ShowErrorMessageAsync(ConexaoHttpClient.PrettyMessage);
            }

            IsLoading = false;

        }, () => {
            return Email.IsValid;
        });

        #endregion

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
