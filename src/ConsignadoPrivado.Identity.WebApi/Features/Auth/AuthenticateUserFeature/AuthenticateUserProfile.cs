using AutoMapper;
using ConsignadoPrivado.Identity.Application.AuthenticateUser;

namespace ConsignadoPrivado.Identity.WebApi.Features.Auth.AuthenticateUserFeature;

public sealed class AuthenticateUserProfile : Profile
{
    public AuthenticateUserProfile()
    {
        CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();
        CreateMap<AuthenticateUserResult, AuthenticateUserResponse>();
    }
}
