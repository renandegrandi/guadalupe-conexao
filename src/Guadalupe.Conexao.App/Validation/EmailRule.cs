using System.Text.RegularExpressions;

namespace Guadalupe.Conexao.App.Validation
{
    public class EmailRule : IValidationRule<string>
    {
        const string pattern = "^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$";

        public string ValidationMessage { get; private set; }

        public EmailRule(string message)
        {
            ValidationMessage = message;
        }

        public bool Check(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            
            return regex.IsMatch(value);
        }
    }
}
