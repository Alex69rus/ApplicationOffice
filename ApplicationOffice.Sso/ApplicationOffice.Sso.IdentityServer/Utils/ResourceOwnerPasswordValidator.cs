using System.Threading.Tasks;
using ApplicationOffice.Sso.Data.Entities;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace ApplicationOffice.Sso.IdentityServer.Utils
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<AoIdentityUser> _userManager;

        public ResourceOwnerPasswordValidator(UserManager<AoIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.FindByNameAsync(context.UserName);
            if (user is null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "Client not found");
                return;
            }

            if (!await _userManager.CheckPasswordAsync(user, context.Password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "Invalid password");
                return;
            }

            var claims = await _userManager.GetClaimsAsync(user);

            context.Result = new GrantValidationResult(
                user.UserName,
                "ResourceOwnerPasswordAndClientCredentials",
                claims
            );
        }
    }
}
