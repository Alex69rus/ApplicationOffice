using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace ApplicationOffice.Sso.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("approvals", "Approvals API scope."),
        };

        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource
            {
                Name = "approvals",
                Description = "The Approvals API",
                Scopes = {"approvals"},
            }
        };

        public static IEnumerable<Client> Clients => new[]
            {
            new Client
            {
                ClientId = "blazor",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                AllowedCorsOrigins = { "https://localhost:5001", "http://localhost:5001" },
                AllowedScopes = { "openid", "profile", "email", "approvals" },
                RedirectUris = { "https://localhost:5001/authentication/login-callback", "http://localhost:5001/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:5001/", "http://localhost:5001/" },
                Enabled = true
            },
        };
    }

    public class ProfileWithRoleIdentityResource : IdentityResources.Profile
    {
        public ProfileWithRoleIdentityResource()
        {
            UserClaims.Add(JwtClaimTypes.Role);
        }
    }
}