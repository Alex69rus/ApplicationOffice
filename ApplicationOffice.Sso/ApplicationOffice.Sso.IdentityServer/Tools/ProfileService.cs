using System.Threading.Tasks;
using ApplicationOffice.Sso.Data.Entities;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using ApplicationOffice.Common.Core.Exceptions;
using IdentityServer4.Extensions;
using ApplicationOffice.Sso.Core.Constants;


namespace ApplicationOffice.Sso.IdentityServer.Tools
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
            var sub = context.Subject.GetSubjectId();
            if (sub is null)
                throw new NotFoundException("User not found", SsoErrorCodes.UserAccountNotFound);

            var user = await _userManager.FindByIdAsync(sub);
            var claims = await _userManager.GetClaimsAsync(user);

            if (claims is not null)
                context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            if (sub is null)
            {
                context.IsActive = false;
                return;
            }

            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user is not null;
        }
    }
}
