using System.Collections.Generic;
using System.ComponentModel;

namespace Guadalupe.Conexao.App.Validation
{
    public interface IValidatable<T> : INotifyPropertyChanged
    {
        List<string> Errors { get; }
        bool IsValid { get; }
        bool IsInvalid { get; }
        bool Validate();
    }
}
