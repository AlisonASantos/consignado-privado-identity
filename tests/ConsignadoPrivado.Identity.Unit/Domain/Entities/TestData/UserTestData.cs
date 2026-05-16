using Bogus;
using ConsignadoPrivado.Identity.Domain.Entities;
using ConsignadoPrivado.Identity.Domain.Enums;

namespace ConsignadoPrivado.Identity.Unit.Domain.Entities.TestData;

public static class UserTestData
{
    private static readonly Faker<User> UserFaker = new Faker<User>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.FullName, f => f.Person.FullName)
        .RuleFor(u => u.Email, f => f.Person.Email)
        .RuleFor(u => u.CPF, f => f.Random.String2(11, "0123456789"))
        .RuleFor(u => u.PasswordHash, f => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(f.Internet.Password())))
        .RuleFor(u => u.Role, f => UserRole.Customer)
        .RuleFor(u => u.Status, f => UserStatus.Active)
        .RuleFor(u => u.CreatedAt, f => DateTime.UtcNow);

    public static User GenerateValid() => UserFaker.Generate();

    public static User GenerateLocked()
    {
        var user = UserFaker.Generate();
        user.LockedUntil = DateTime.UtcNow.AddMinutes(30);
        user.FailedLoginAttempts = 5;
        return user;
    }

    public static User GenerateInactive()
    {
        var user = UserFaker.Generate();
        user.Status = UserStatus.Inactive;
        return user;
    }

    public static User GenerateBlocked()
    {
        var user = UserFaker.Generate();
        user.Status = UserStatus.Blocked;
        return user;
    }
}
