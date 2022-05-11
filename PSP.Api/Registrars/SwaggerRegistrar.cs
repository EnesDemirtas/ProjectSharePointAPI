﻿using PSP.Api.Options;

namespace PSP.Api.Registrars {

    public class SwaggerRegistrar : IWebApplicationBuilderRegistrar {

        public void RegisterServices(WebApplicationBuilder builder) {
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
        }
    }
}