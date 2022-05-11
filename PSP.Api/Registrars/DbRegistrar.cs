using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PSP.Dal;

namespace PSP.Api.Registrars {

    public class DbRegistrar : IWebApplicationBuilderRegistrar {

        public void RegisterServices(WebApplicationBuilder builder) {
            var cs = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<DataContext>(options => {
                options.UseSqlServer(cs);
            });

            builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<DataContext>();
        }
    }
}