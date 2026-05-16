using AutoMapper;
using ConsignadoPrivado.Identity.Domain.Entities;

namespace ConsignadoPrivado.Identity.Application.CreateUser;

public class CreateUserProfile : Profile
{
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        CreateMap<User, CreateUserResult>();
    }
}
