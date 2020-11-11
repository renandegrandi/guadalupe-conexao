using Guadalupe.Conexao.App.Repository;
using Guadalupe.Conexao.App.Repository.DTO;
using Guadalupe.Conexao.App.Validation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    sealed class ValidateCodeViewModel: ViewModel
    {
        #region Dependencies

        private readonly CancellationToken _cancellationToken;

        #endregion

        #region Constructor
        public ValidateCodeViewModel(INavigation navigation, string email) : base(navigation)
        {
            _cancellationToken = new CancellationToken();
            Email = email;

            Code = new ValidatableObject<string>(new IsNotNullOrEmptyRule("É necessário informar o código que foi enviado no seu email."));
        }
        #endregion

        public string Email { get; private set; }
        public ValidatableObject<string> Code { get; set; }
        public string Message { get; set; }
        public bool HasMessage { 
            get 
            {
                return !string.IsNullOrWhiteSpace(Message);
            } 
        }
        public ICommand OnSendNewCodeCommand => new Command(async () =>
        {
            await Task.CompletedTask;
        });
        public ICommand OnValidateCodeCommand => new Command(async () =>
        {
            try
            {
                await ConexaoHttpClient.AuthenticationAsync(new AuthenticationDto { 
                     GrantType = AuthenticationDto.GrantTypes.password,
                     Username = this.Email,
                     Password = this.Code.Value
                }, this._cancellationToken);

            }
            catch (System.Exception ex)
            {
                Message = ex.Message;

                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }
        });
    }
}
