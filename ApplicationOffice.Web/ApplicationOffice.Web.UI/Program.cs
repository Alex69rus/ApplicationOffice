using System.Net.Http;
using System.Threading.Tasks;
using ApplicationOffice.Web.UI.Tools;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationOffice.Web.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services
                .AddHttpClient("api")
                .AddHttpMessageHandler(sp => sp
                    .GetRequiredService<AuthorizationMessageHandler>()
                    .ConfigureHandler(
                        authorizedUrls: new[] { "https://localhost:5002" },
                        scopes: new[] { "weatherapi" }));

            builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("api"));

            builder.Services
                .AddOidcAuthentication(options =>
                {
                    builder.Configuration.Bind("oidc", options.ProviderOptions);
                })
                .AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();


            await builder.Build().RunAsync();
        }
    }
}
