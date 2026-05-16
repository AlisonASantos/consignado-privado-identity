using FluentValidation.Results;

namespace ConsignadoPrivado.Common.Validation;

public class ValidationErrorDetail
{
    public string PropertyName { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;

    public static explicit operator ValidationErrorDetail(ValidationFailure failure)
    {
        return new ValidationErrorDetail
        {
            PropertyName = failure.PropertyName,
            ErrorMessage = failure.ErrorMessage
        };
    }
}
