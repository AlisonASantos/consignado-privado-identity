using Bogus;
using ConsignadoPrivado.Identity.Application.CreateUser;
using ConsignadoPrivado.Identity.Domain.Enums;

namespace ConsignadoPrivado.Identity.Unit.Application.CreateUser.TestData;

public static class CreateUserHandlerTestData
{
    private static readonly Faker<CreateUserCommand> CommandFaker = new Faker<CreateUserCommand>()
        .RuleFor(c => c.FullName, f => f.Person.FullName)
        .RuleFor(c => c.Email, f => f.Person.Email)
        .RuleFor(c => c.CPF, f => f.Random.String2(11, "0123456789"))
        .RuleFor(c => c.Password, f => f.Internet.Password(10))
        .RuleFor(c => c.Role, f => UserRole.Customer);

    public static CreateUserCommand GenerateValidCommand() => CommandFaker.Generate();

    public static CreateUserCommand GenerateInvalidCommand()
    {
        return new CreateUserCommand
        {
            FullName = "",
            Email = "invalid",
            CPF = "123",
            Password = "short",
            Role = UserRole.Customer
        };
    }
}
