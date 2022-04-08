using System.ComponentModel.DataAnnotations;

namespace Guadalupe.Conexao.Api.Domain.Validation
{
    public class GrantTypeValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("A propriedade grant_type é obrigatória!");

            var valueConverted = (GrantTypes)value;

            var body = validationContext.ObjectInstance;

            switch (valueConverted)
            {
                case GrantTypes.password:

                    var username = (string)body.GetType().GetProperty("Username").GetValue(body);
                    var password = (string)body.GetType().GetProperty("Password").GetValue(body);

                    if (string.IsNullOrWhiteSpace(username))
                        return new ValidationResult("A propriedade (username) é obrigatória!");

                    if (string.IsNullOrWhiteSpace(password))
                        return new ValidationResult("A propriedade (password) é obrigatória!");

                    break;
                case GrantTypes.refresh_token:

                    var refreshtoken = (string)body.GetType().GetProperty("RefreshToken").GetValue(body);

                    if (string.IsNullOrWhiteSpace(refreshtoken))
                        return new ValidationResult("A propriedade (refreshtoken) é obrigatória!");

                    break;
                default:
                    break;
            }

            return ValidationResult.Success;
        }
    }
}
