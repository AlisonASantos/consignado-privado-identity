using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using ConsignadoPrivado.Identity.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsignadoPrivado.Identity.Application.AuthenticateUser;

public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResult>
{
    private readonly IUserRepository _repository;
    private readonly ILogger<AuthenticateUserHandler> _logger;
    private readonly IConfiguration _configuration;

    public AuthenticateUserHandler(IUserRepository repository, ILogger<AuthenticateUserHandler> logger, IConfiguration configuration)
    {
        _repository = repository;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<AuthenticateUserResult> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new AuthenticateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = await _repository.GetByEmailAsync(command.Email, cancellationToken);
        if (user == null)
            throw new UnauthorizedAccessException("Credenciais inválidas");

        if (user.IsLocked())
            throw new UnauthorizedAccessException("Conta bloqueada temporariamente. Tente novamente mais tarde.");

        if (!user.IsActive())
            throw new UnauthorizedAccessException("Conta inativa ou bloqueada");

        if (!BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash))
        {
            user.RecordFailedLogin();
            await _repository.UpdateAsync(user, cancellationToken);
            throw new UnauthorizedAccessException("Credenciais inválidas");
        }

        user.RecordSuccessfulLogin();
        var refreshToken = Guid.NewGuid().ToString("N");
        user.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(7));
        await _repository.UpdateAsync(user, cancellationToken);

        _logger.LogInformation("UserAuthenticated: {UserId}, Email: {Email}", user.Id, user.Email);

        var expiresAt = DateTime.UtcNow.AddHours(1);

        return new AuthenticateUserResult
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            AccessToken = GenerateJwtToken(user.Id, user.Email, user.Role.ToString(), expiresAt, _configuration),
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt
        };
    }

    private static string GenerateJwtToken(Guid userId, string email, string role, DateTime expiresAt, IConfiguration configuration)
    {
        var jwtSecret = configuration["Jwt:SecretKey"] 
            ?? throw new InvalidOperationException("JWT SecretKey not configured. Set 'Jwt:SecretKey' in appsettings or environment.");
        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(jwtSecret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "ConsignadoPrivado.Identity",
            audience: "ConsignadoPrivado",
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
