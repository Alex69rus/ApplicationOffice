using System.Collections.Generic;
using IdentityServer4.Models;

namespace ApplicationOffice.Sso.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes => new[] { new ApiScope("sso", "SSO scope.") };

        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource
            {
                Name = "sso-gateway-api",
                Description = "SSO gateway resource",
                ApiSecrets = {new Secret("secret".Sha256())},
                Scopes = {"sso"},
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
        };
    }
}
