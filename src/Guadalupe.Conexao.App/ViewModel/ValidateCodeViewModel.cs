using Guadalupe.Conexao.App.Extensions;
using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository;
using Guadalupe.Conexao.App.Repository.DTO;
using Guadalupe.Conexao.App.Service;
using Guadalupe.Conexao.App.Validation;
using Guadalupe.Conexao.App.View;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Guadalupe.Conexao.App.ViewModel
{
    sealed class ValidateCodeViewModel: ViewModel
    {
        #region Dependencies

        private readonly IUserRepository _userRepository;
        private readonly ISessionService _sessionService;

        #endregion

        #region Constructor
        public ValidateCodeViewModel(INavigation navigation, 
            IUserRepository userRepository, 
            ISessionService sessionService,
            IPopupService popupService,
            string email) : base(navigation, popupService)
        {
            _userRepository = userRepository;
            _sessionService = sessionService;

            Email = email;
            Code = new ValidatableObject<string>(new IsNotNullOrEmptyRule("É necessário informar o código que foi enviado no seu email."));
        }
        #endregion

        #region Fields

        private bool _isLoading;

        #endregion

        #region Properties
        public string Email { get; private set; }
        public ValidatableObject<string> Code { get; set; }
        public bool IsLoading {
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

        #endregion

        #region Commands

        public ICommand OnSendNewCodeCommand => new Command(async () =>
        {
            try
            {

                await ConexaoHttpClient.SendNewCodeByEmailAsync(Email, _cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (System.Exception)
            {
                await this._popupService.ShowErrorMessageAsync(ConexaoHttpClient.PrettyMessage);
            }
        });
        public ICommand OnValidateCodeCommand => new Command(async () =>
        {
            try
            {
                IsLoading = true;

                var apiAuth = await ConexaoHttpClient.AuthenticationAsync(new AuthenticationDto
                {
                    Username = Email,
                    Password = Code.Value
                }, _cancellationToken);

                var person = new Person(apiAuth.UserInfo.Id,
                    apiAuth.UserInfo.Email,
                    apiAuth.UserInfo.ProfileImage,
                    apiAuth.UserInfo.Name);

                var user = new User(person, apiAuth.AccessToken, apiAuth.RefreshToken);

                await _userRepository.SaveAsync(user);

                // é necessário atualizar, para vincular com as informações da mobile-info.
                user = await _userRepository.GetAsync();

                _sessionService.SetUser(user);

                if (!string.IsNullOrWhiteSpace(user.MobileInfo?.FCMToken))
                {
                    ConexaoHttpClient.RegisterFirebaseTokenAsync(user.MobileInfo.FCMToken, _cancellationToken)
                        .SafeFireAndForget(false);
                }

                await _navigation.PushAsync(new MainView());
            }
            catch (UnauthorizedException ex)
            {
                await this._popupService.ShowErrorMessageAsync(ex.Message);
            }
            catch (System.Exception ex)
            {
                Log.Warning(nameof(ValidateCodeViewModel), ex.Message);

                await this._popupService.ShowErrorMessageAsync(ConexaoHttpClient.PrettyMessage);
            }

            IsLoading = false;

        }, () => {
            return Code.IsValid;
        });

        #endregion
    }
}
