using MediatR;

namespace ConsignadoPrivado.Identity.Application.GetUser;

public class GetUserCommand : IRequest<GetUserResult>
{
    public Guid Id { get; set; }
}
