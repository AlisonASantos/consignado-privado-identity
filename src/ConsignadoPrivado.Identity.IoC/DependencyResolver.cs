using ConsignadoPrivado.Identity.Domain.Repositories;
using ConsignadoPrivado.Identity.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ConsignadoPrivado.Identity.IoC;

public static class DependencyResolver
{
    public static void RegisterIdentityDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
}
