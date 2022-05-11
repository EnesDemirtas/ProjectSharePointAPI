namespace PSP.Api.Registrars {

    public class MvcWebAppRegistrar : IWebApplicationRegistrar {

        public void RegisterPipelineComponents(WebApplication app) {
            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}