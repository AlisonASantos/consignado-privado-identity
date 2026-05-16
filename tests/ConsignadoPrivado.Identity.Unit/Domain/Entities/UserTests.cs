using ConsignadoPrivado.Identity.Domain.Enums;
using ConsignadoPrivado.Identity.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace ConsignadoPrivado.Identity.Unit.Domain.Entities;

public class UserTests
{
    [Fact(DisplayName = "Given valid data When creating user Then status is PendingVerification")]
    public void CreateUser_ValidData_StatusPending()
    {
        var user = new Identity.Domain.Entities.User();
        user.Status.Should().Be(UserStatus.PendingVerification);
    }

    [Fact(DisplayName = "Given user When activating Then status is Active")]
    public void Activate_User_StatusActive()
    {
        var user = UserTestData.GenerateValid();
        user.Status = UserStatus.PendingVerification;

        user.Activate();

        user.Status.Should().Be(UserStatus.Active);
    }

    [Fact(DisplayName = "Given user When blocking Then status is Blocked")]
    public void Block_User_StatusBlocked()
    {
        var user = UserTestData.GenerateValid();

        user.Block("Suspicious activity");

        user.Status.Should().Be(UserStatus.Blocked);
    }

    [Fact(DisplayName = "Given active user When recording successful login Then resets failed attempts")]
    public void RecordSuccessfulLogin_ActiveUser_ResetsFailedAttempts()
    {
        var user = UserTestData.GenerateValid();
        user.FailedLoginAttempts = 3;

        user.RecordSuccessfulLogin();

        user.FailedLoginAttempts.Should().Be(0);
        user.LastLoginAt.Should().NotBeNull();
        user.LockedUntil.Should().BeNull();
    }

    [Fact(DisplayName = "Given user with 4 failed attempts When recording failed login Then locks account")]
    public void RecordFailedLogin_FourthAttempt_LocksAccount()
    {
        var user = UserTestData.GenerateValid();
        user.FailedLoginAttempts = 4;

        user.RecordFailedLogin();

        user.FailedLoginAttempts.Should().Be(5);
        user.LockedUntil.Should().NotBeNull();
        user.LockedUntil.Should().BeAfter(DateTime.UtcNow);
    }

    [Fact(DisplayName = "Given locked user When checking IsLocked Then returns true")]
    public void IsLocked_LockedUser_ReturnsTrue()
    {
        var user = UserTestData.GenerateLocked();

        user.IsLocked().Should().BeTrue();
    }

    [Fact(DisplayName = "Given user When setting refresh token Then token is stored")]
    public void SetRefreshToken_ValidToken_TokenStored()
    {
        var user = UserTestData.GenerateValid();
        var token = "test-refresh-token";
        var expiresAt = DateTime.UtcNow.AddDays(7);

        user.SetRefreshToken(token, expiresAt);

        user.RefreshToken.Should().Be(token);
        user.RefreshTokenExpiresAt.Should().Be(expiresAt);
    }

    [Fact(DisplayName = "Given valid refresh token When validating Then returns true")]
    public void IsValidRefreshToken_ValidToken_ReturnsTrue()
    {
        var user = UserTestData.GenerateValid();
        var token = "valid-token";
        user.SetRefreshToken(token, DateTime.UtcNow.AddDays(7));

        user.IsValidRefreshToken(token).Should().BeTrue();
    }

    [Fact(DisplayName = "Given user When revoking refresh token Then token is null")]
    public void RevokeRefreshToken_HasToken_TokenNull()
    {
        var user = UserTestData.GenerateValid();
        user.SetRefreshToken("token", DateTime.UtcNow.AddDays(7));

        user.RevokeRefreshToken();

        user.RefreshToken.Should().BeNull();
        user.RefreshTokenExpiresAt.Should().BeNull();
    }

    [Fact(DisplayName = "Given valid user When validating Then returns valid")]
    public void Validate_ValidUser_ReturnsValid()
    {
        var user = UserTestData.GenerateValid();

        var result = user.Validate();

        result.IsValid.Should().BeTrue();
    }
}
