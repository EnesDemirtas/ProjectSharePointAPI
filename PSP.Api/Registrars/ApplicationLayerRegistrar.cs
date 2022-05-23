using PSP.Application.Services;

namespace PSP.Api.Registrars; 

public class ApplicationLayerRegistrar : IWebApplicationBuilderRegistrar {

    public void RegisterServices(WebApplicationBuilder builder) {
        builder.Services.AddScoped<IdentityService>();
    }
}