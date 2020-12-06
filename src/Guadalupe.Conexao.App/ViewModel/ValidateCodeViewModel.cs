using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository;
using Guadalupe.Conexao.App.Repository.DTO;
using Guadalupe.Conexao.App.Service;
using Guadalupe.Conexao.App.Validation;
using Guadalupe.Conexao.App.View;
using System.Windows.Input;
using Xamarin.Forms;

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
            string email) : base(navigation)
        {
            _userRepository = userRepository;
            _sessionService = sessionService;

            Email = email;
            Code = new ValidatableObject<string>(new IsNotNullOrEmptyRule("É necessário informar o código que foi enviado no seu email."));
        }
        #endregion

        #region Properties
        public string Email { get; private set; }
        public ValidatableObject<string> Code { get; set; }
        public string Message { get; set; }
        public bool HasMessage
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Message);
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
                Message = ConexaoHttpClient.PrettyMessage;

                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }
        });
        public ICommand OnValidateCodeCommand => new Command(async () =>
        {
            try
            {
                Message = string.Empty;

                OnPropertyChanged(nameof(Message));

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

                _sessionService.SetUser(user);

                await _navigation.PushAsync(new MainView());
            }
            catch (UnauthorizedException ex)
            {
                Message = ex.Message;

                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }
            catch (System.Exception ex)
            {
                Message = ConexaoHttpClient.PrettyMessage;

                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }
        }, () => {
            return Code.IsValid;
        });

        #endregion
    }
}
