using PSP.Api.Filters;

namespace PSP.Api.Registrars {

    public class MvcRegistrar : IWebApplicationBuilderRegistrar {

        public void RegisterServices(WebApplicationBuilder builder) {
            builder.Services.AddControllers(config => { config.Filters.Add(typeof(PSPExceptionHandler)); });

            builder.Services.AddEndpointsApiExplorer();
        }
    }
}