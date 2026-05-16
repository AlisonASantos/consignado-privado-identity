using ConsignadoPrivado.Identity.ORM;
using Microsoft.EntityFrameworkCore;

namespace ConsignadoPrivado.Identity.WebApi.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app) 
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using IdentityContext dbcontext = scope.ServiceProvider.GetRequiredService<IdentityContext>();

            dbcontext.Database.Migrate();
        }
    }
}
