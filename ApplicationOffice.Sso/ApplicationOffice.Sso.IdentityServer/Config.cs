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

                // // let's include the role claim in the profile
                // new ProfileWithRoleIdentityResource(),

                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("sso", "SSO scope."),
            new ApiScope("weatherapi", "Weather API scope."), 
        };

        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource
            {
                Name = "sso-gateway-api",
                Description = "SSO gateway resource",
                ApiSecrets = {new Secret("secret".Sha256())},
                Scopes = {"sso"},
            },
            // new ApiResource("weatherapi", "The Weather API", new[] { JwtClaimTypes.Role }),
            new ApiResource()
            {
                Name = "weatherapi",
                Description = "The Weather API",
                Scopes = {"weatherapi"},
            },
        };

    public static IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                ClientId = "sso-gateway-client",
                ClientSecrets = {new Secret("secret".Sha256())},
                AccessTokenType = AccessTokenType.Reference,
                AllowOfflineAccess = true,
                UpdateAccessTokenClaimsOnRefresh = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = {"sso"},
            },
            new Client
            {
                ClientId = "blazor",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                AllowedCorsOrigins = { "https://localhost:5001" },
                AllowedScopes = { "openid", "profile", "email", "weatherapi" },
                RedirectUris = { "https://localhost:5001/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:5001/" },
                Enabled = true
            },
        };
    }

    public class ProfileWithRoleIdentityResource : IdentityResources.Profile
    {
        public ProfileWithRoleIdentityResource()
        {
            this.UserClaims.Add(JwtClaimTypes.Role);
        }
    }
}