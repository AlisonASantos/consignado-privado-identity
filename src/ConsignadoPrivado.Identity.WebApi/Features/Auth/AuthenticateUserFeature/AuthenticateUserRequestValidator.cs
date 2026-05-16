using FluentValidation;

namespace ConsignadoPrivado.Identity.WebApi.Features.Auth.AuthenticateUserFeature;

public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
{
    public AuthenticateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-mail é obrigatório")
            .EmailAddress()
            .WithMessage("E-mail inválido");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatória");
    }
}
