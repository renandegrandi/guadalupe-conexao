namespace Guadalupe.Conexao.App.Validation
{
    public class IsNotNullOrEmptyRule : IValidationRule<string>
    {
        public string ValidationMessage { get; private set; }

        public IsNotNullOrEmptyRule(string message)
        {
            ValidationMessage = message;
        }

        public bool Check(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
