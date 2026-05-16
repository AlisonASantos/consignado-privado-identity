using Microsoft.AspNetCore.Builder;

namespace ConsignadoPrivado.Identity.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
