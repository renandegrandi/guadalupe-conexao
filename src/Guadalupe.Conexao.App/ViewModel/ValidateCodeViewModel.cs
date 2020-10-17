using Guadalupe.Conexao.App.Validation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    sealed class ValidateCodeViewModel: ViewModel
    {
        #region Dependencias
        private string InternalCode { get; set; }

        #endregion

        public ValidateCodeViewModel(INavigation navigation, string code) : base(navigation)
        {
            Code = new ValidatableObject<string>(new IsNotNullOrEmptyRule("É necessário informar o código que foi enviado no seu email."));

            InternalCode = code;
        }

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
            if (!Code.Equals(InternalCode)) 
            {
                Message = "Código ínvalido!";

                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }

            //TODO: Verificar se será necessário validar no servidor o código do usuário.
            //TODO: Implementar entrada no aplicativo.

            await Task.CompletedTask;
        });
    }
}
