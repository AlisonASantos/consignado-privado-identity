using ConsignadoPrivado.Identity.Domain.Repositories;
using ConsignadoPrivado.Identity.ORM;
using ConsignadoPrivado.Identity.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConsignadoPrivado.Identity.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<IdentityContext>());
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
}
