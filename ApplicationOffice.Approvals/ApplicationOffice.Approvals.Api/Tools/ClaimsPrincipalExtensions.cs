using System.Security.Claims;
using ApplicationOffice.Common.Core.Constants;
using ApplicationOffice.Common.Core.Exceptions;

namespace ApplicationOffice.Approvals.Api.Tools
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetUserIdOrThrow(this ClaimsPrincipal claims)
        {
            var value = claims.FindFirstValue(AoClaims.UserId)
                ?? throw new UnauthorizedException("Unauthorized access", DefaultErrorCodes.Unauthorized);

            if (!long.TryParse(value, out var userId))
                throw new UnauthorizedException("Unauthorized access", DefaultErrorCodes.Unauthorized);

            return userId;
        }

        public static string GetNameOrThrow(this ClaimsPrincipal claims)
        {
            return claims.FindFirstValue(AoClaims.UserName)
                ?? throw new UnauthorizedException("Unauthorized access", DefaultErrorCodes.Unauthorized);
        }
    }
}
