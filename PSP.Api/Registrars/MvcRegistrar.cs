namespace PSP.Api.Registrars
{

    public class MvcRegistrar : IWebApplicationBuilderRegistrar
    {

        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(config => { config.Filters.Add(typeof(PSPExceptionHandler)); });


            builder.Services.AddCors(c => {c.AddPolicy("AllowOrigin", 
                options => options.AllowAnyOrigin());});

            builder.Services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            builder.Services.AddVersionedApiExplorer(config =>
            {
                config.GroupNameFormat = "'v'VVV";
                config.SubstituteApiVersionInUrl = true;
            });
            builder.Services.AddEndpointsApiExplorer();
        }
    }
}