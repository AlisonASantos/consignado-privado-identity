using AutoMapper;
using ConsignadoPrivado.Identity.Application.GetUser;

namespace ConsignadoPrivado.Identity.WebApi.Features.Users.GetUserFeature;

public sealed class GetUserProfile : Profile
{
    public GetUserProfile()
    {
        CreateMap<GetUserRequest, GetUserCommand>();
        CreateMap<GetUserResult, GetUserResponse>();
    }
}
