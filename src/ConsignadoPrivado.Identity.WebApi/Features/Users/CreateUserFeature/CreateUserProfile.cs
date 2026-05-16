using AutoMapper;
using ConsignadoPrivado.Identity.Application.CreateUser;

namespace ConsignadoPrivado.Identity.WebApi.Features.Users.CreateUserFeature;

public sealed class CreateUserProfile : Profile
{
    public CreateUserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<CreateUserResult, CreateUserResponse>();
    }
}
