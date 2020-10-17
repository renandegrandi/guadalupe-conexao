using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Guadalupe.Conexao.App.Validation
{
    public class ValidatableObject<T> : IValidatable<T>
    {
        #region Construtores

        private ValidatableObject()
        {
            Errors = new List<string>();
            IsTouched = false;
        }
        public ValidatableObject(List<IValidationRule<T>> rules) : this()
        {
            Validations = rules;

            Validate();
        }
        public ValidatableObject(IValidationRule<T> rule) : this()
        {
            Validations = new List<IValidationRule<T>> {
                rule
            };

            Validate();
        }

        #endregion

        #region Membros Privados

        List<IValidationRule<T>> Validations { get; set; }
        bool _isTouched;
        T _value;

        #endregion

        #region Propriedades

        public List<string> Errors { get; private set; }
        public bool IsValid { get; private set; }
        public bool IsInvalid
        {
            get
            {
                return !IsValid;
            }
        }
        public bool IsTouched
        {
            get
            {
                return _isTouched;
            }
            set
            {
                _isTouched = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTouched)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowError)));
            }
        }
        public bool ShowError
        {
            get
            {
                return (IsTouched && IsInvalid);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;

                var valid = Validate();

                if (valid != IsValid)
                {
                    IsValid = valid;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsInvalid)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowError)));
                }
            }
        }

        #endregion

        #region Métodos Públicos

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();

            return !Errors.Any();
        }

        #endregion
    }
}
