using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationOffice.Sso.Data.Entities;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using ApplicationOffice.Common.Core.Exceptions;

namespace ApplicationOffice.Sso.IdentityServer.Utils
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<AoIdentityUser> _userManager;

        public ProfileService(UserManager<AoIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userName = GetUserName(context.Subject);
            if (userName is null)
                throw new NotFoundException("User not found");

            var user = await _userManager.FindByNameAsync(userName);
            var claims = await _userManager.GetClaimsAsync(user);

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userName = GetUserName(context.Subject);
            if (userName is null)
            {
                context.IsActive = false;
                return;
            }

            var user = await _userManager.FindByNameAsync(userName);
            context.IsActive = user is not null;
        }

        private static string? GetUserName(ClaimsPrincipal subject)
        {
            return subject?.Claims?.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject)?.Value;
        }
    }
}
