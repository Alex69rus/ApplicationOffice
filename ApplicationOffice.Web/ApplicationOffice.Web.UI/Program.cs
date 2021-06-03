using System.Net.Http;
using System.Threading.Tasks;
using ApplicationOffice.Web.UI.Configurations;
using ApplicationOffice.Web.UI.Tools;
using MatBlazor;
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


            var approvalsApiOpts = builder
                .Configuration
                .GetSection(nameof(ApprovalsApiOptions))
                .Get<ApprovalsApiOptions>();
            builder.Services.AddSingleton(approvalsApiOpts);

            builder.Services
                .AddHttpClient("api")
                .AddHttpMessageHandler(sp => sp
                    .GetRequiredService<AuthorizationMessageHandler>()
                    .ConfigureHandler(
                        authorizedUrls: new[] {approvalsApiOpts.Address.ToString()},
                        scopes: new[] {approvalsApiOpts.Scope}));

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("api"));

            builder.Services
                .AddOidcAuthentication(options => { builder.Configuration.Bind("oidc", options.ProviderOptions); })
                .AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.TopRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });

            await builder.Build().RunAsync();
        }
    }
}
