using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ApplicationOffice.Sso.Core.Tools
{
    public static class IdentityResultExtensions
    {
        /// <summary>
        /// Create ValidationException from IdentityResult
        /// </summary>
        /// <param name="result">Identity result</param>
        /// <returns></returns>
        public static ValidationException ToException(this IdentityResult result)
        {
            return new ValidationException(
                string.Join("; ", result.Errors.Select(x => $"'{x.Code}: {x.Description}'"))
            );
        }

        /// <summary>
        /// Validate IdentityResult.Succeeded. If not - throw ValidationException
        /// </summary>
        /// <param name="result">Identity result</param>
        public static void ValidateOrThrow(this IdentityResult result)
        {
            if (!result.Succeeded)
                throw result.ToException();
        }
    }
}
