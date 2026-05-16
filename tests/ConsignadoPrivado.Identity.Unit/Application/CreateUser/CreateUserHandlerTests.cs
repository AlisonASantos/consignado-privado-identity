using AutoMapper;
using ConsignadoPrivado.Identity.Application.CreateUser;
using ConsignadoPrivado.Identity.Domain.Entities;
using ConsignadoPrivado.Identity.Domain.Repositories;
using ConsignadoPrivado.Identity.Unit.Application.CreateUser.TestData;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace ConsignadoPrivado.Identity.Unit.Application.CreateUser;

public class CreateUserHandlerTests
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly CreateUserHandler _handler;

    public CreateUserHandlerTests()
    {
        _repository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateUserHandler>>();
        _handler = new CreateUserHandler(_repository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid command When handling Then creates user successfully")]
    public async Task Handle_ValidCommand_CreatesUser()
    {
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        var user = new User { Id = Guid.NewGuid(), FullName = command.FullName, Email = command.Email, CPF = command.CPF };
        var expectedResult = new CreateUserResult { Id = user.Id, FullName = command.FullName, Email = command.Email };

        _mapper.Map<User>(command).Returns(user);
        _mapper.Map<CreateUserResult>(user).Returns(expectedResult);
        _repository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns((User?)null);
        _repository.GetByCPFAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns((User?)null);
        _repository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(user);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.FullName.Should().Be(command.FullName);
        result.Email.Should().Be(command.Email);
    }

    [Fact(DisplayName = "Given invalid command When handling Then throws ValidationException")]
    public async Task Handle_InvalidCommand_ThrowsValidation()
    {
        var command = CreateUserHandlerTestData.GenerateInvalidCommand();

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given existing email When handling Then throws InvalidOperationException")]
    public async Task Handle_ExistingEmail_ThrowsException()
    {
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        var existingUser = new User { Email = command.Email };
        _repository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(existingUser);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*e-mail*");
    }
}
