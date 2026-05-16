using AutoMapper;
using MediatR;
using ConsignadoPrivado.Identity.Domain.Repositories;

namespace ConsignadoPrivado.Identity.Application.GetUser;

public class GetUserHandler : IRequestHandler<GetUserCommand, GetUserResult>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUserHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetUserResult> Handle(GetUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"Usuário com ID {command.Id} não encontrado");

        return _mapper.Map<GetUserResult>(user);
    }
}
