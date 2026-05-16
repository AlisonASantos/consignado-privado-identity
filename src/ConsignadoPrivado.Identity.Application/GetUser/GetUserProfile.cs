using AutoMapper;
using ConsignadoPrivado.Identity.Domain.Entities;

namespace ConsignadoPrivado.Identity.Application.GetUser;

public class GetUserProfile : Profile
{
    public GetUserProfile()
    {
        CreateMap<User, GetUserResult>();
    }
}
