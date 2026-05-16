using AutoMapper;
using FluentValidation;
using MediatR;
using ConsignadoPrivado.Identity.Domain.Entities;
using ConsignadoPrivado.Identity.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace ConsignadoPrivado.Identity.Application.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(IUserRepository repository, IMapper mapper, ILogger<CreateUserHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingUser = await _repository.GetByEmailAsync(command.Email, cancellationToken);
        if (existingUser != null)
            throw new InvalidOperationException("Já existe um usuário com este e-mail");

        var existingCPF = await _repository.GetByCPFAsync(command.CPF, cancellationToken);
        if (existingCPF != null)
            throw new InvalidOperationException("Já existe um usuário com este CPF");

        var user = _mapper.Map<User>(command);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);
        user.Activate();

        var created = await _repository.CreateAsync(user, cancellationToken);
        _logger.LogInformation("UserCreated: {UserId}, Email: {Email}", created.Id, created.Email);

        return _mapper.Map<CreateUserResult>(created);
    }
}
