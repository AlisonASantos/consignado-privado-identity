using FluentValidation;

namespace ConsignadoPrivado.Identity.WebApi.Features.Users.CreateUserFeature;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Nome completo é obrigatório")
            .MinimumLength(3)
            .WithMessage("Nome completo deve ter no mínimo 3 caracteres")
            .MaximumLength(200)
            .WithMessage("Nome completo deve ter no máximo 200 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-mail é obrigatório")
            .EmailAddress()
            .WithMessage("E-mail inválido");

        RuleFor(x => x.CPF)
            .NotEmpty()
            .WithMessage("CPF é obrigatório")
            .Length(11)
            .WithMessage("CPF deve ter 11 dígitos");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatória")
            .MinimumLength(8)
            .WithMessage("Senha deve ter no mínimo 8 caracteres");
    }
}
