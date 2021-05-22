using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationOffice.Sso.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ApplicationOffice.Sso.IdentityServer.Tools
{
    public class AoPasswordValidator : PasswordValidator<AoIdentityUser>
    {
        const int MaxPasswordLength = 30;
        public override async Task<IdentityResult> ValidateAsync(
            UserManager<AoIdentityUser> manager,
            AoIdentityUser user,
            string password)
        {
            IdentityResult result = await base.ValidateAsync(manager, user, password);
            List<IdentityError> errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

            if (password?.Length > MaxPasswordLength)
            {
                errors.Add(new IdentityError
                {
                    Description = "Password too long",
                    Code = "passwordtoolong"
                });
            }

            return errors.Any() ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }
    }
}