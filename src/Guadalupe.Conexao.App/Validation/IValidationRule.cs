namespace Guadalupe.Conexao.App.Validation
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; }
        bool Check(T value);
    }
}
