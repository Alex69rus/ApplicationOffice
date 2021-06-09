using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApplicationOffice.Common.Core.Constants;
using ApplicationOffice.Common.Core.Exceptions;
using ApplicationOffice.Sso.Core.Constants;
using Microsoft.AspNetCore.Identity;

namespace ApplicationOffice.Sso.Core.Tools
{
    public static class IdentityResultExtensions
    {
        /// <summary>
        /// Create BadRequestException from IdentityResult
        /// </summary>
        /// <param name="result">Identity result</param>
        /// <returns></returns>
        public static BadRequestException ToException(this IdentityResult result)
        {
            var error = result.Errors.FirstOrDefault();
            return new BadRequestException(
                error?.Description ?? "Invalid request.",
                GetErrorCode(error?.Code));
        }

        /// <summary>
        /// Validate IdentityResult.Succeeded. If not - throw BadRequestException
        /// </summary>
        /// <param name="result">Identity result</param>
        public static void ValidateOrThrow(this IdentityResult result)
        {
            if (!result.Succeeded)
                throw result.ToException();
        }
        
        private static int GetErrorCode(string? identityErrorCode) => identityErrorCode?.ToLowerInvariant() switch
        {
            "defaulterror" => DefaultErrorCodes.BadRequest,
            "duplicateemail" => DefaultErrorCodes.BadRequest,
            "duplicatename" => DefaultErrorCodes.BadRequest,
            "externalloginexists" => DefaultErrorCodes.BadRequest,
            "invalidemail" => DefaultErrorCodes.BadRequest,
            "invalidtoken" => DefaultErrorCodes.BadRequest,
            "invalidusername" => DefaultErrorCodes.BadRequest,
            "lockoutnotenabled" => DefaultErrorCodes.BadRequest,
            "notokenprovider" => DefaultErrorCodes.BadRequest,
            "notwofactorprovider" => DefaultErrorCodes.BadRequest,
            "passwordmismatch" => SsoErrorCodes.InvalidPassword,
            "passwordrequiresdigit" => SsoErrorCodes.PasswordRequiresDigit,
            "passwordrequireslower" => SsoErrorCodes.PasswordRequiresLower,
            "passwordrequiresnonalphanumeric" => SsoErrorCodes.PasswordRequiresNonAlphanumeric,
            "passwordrequiresupper" => SsoErrorCodes.PasswordRequiresUpper,
            "passwordtooshort" => SsoErrorCodes.PasswordTooShort,
            "passwordtoolong" => SsoErrorCodes.PasswordTooLong,
            "propertytooshort" => DefaultErrorCodes.BadRequest,
            "rolenotfound" => DefaultErrorCodes.NotFound,
            "storenotiqueryablerolestore" => DefaultErrorCodes.Internal,
            "storenotiqueryableuserstore" => DefaultErrorCodes.Internal,
            "storenotiuserclaimstore" => DefaultErrorCodes.Internal,
            "storenotiuserconfirmationstore" => DefaultErrorCodes.Internal,
            "storenotiuseremailstore" => DefaultErrorCodes.Internal,
            "storenotiuserlockoutstore" => DefaultErrorCodes.Internal,
            "storenotiuserloginstore" => DefaultErrorCodes.Internal,
            "storenotiuserpasswordstore" => DefaultErrorCodes.Internal,
            "storenotiuserphonenumberstore" => DefaultErrorCodes.Internal,
            "storenotiuserrolestore" => DefaultErrorCodes.Internal,
            "storenotiusersecuritystampstore" => DefaultErrorCodes.Internal,
            "storenotiusertwofactorstore" => DefaultErrorCodes.Internal,
            "useralreadyhaspassword" => DefaultErrorCodes.BadRequest,
            "useralreadyinrole" => DefaultErrorCodes.BadRequest,
            "useridnotfound" => DefaultErrorCodes.NotFound,
            "usernamenotfound" => DefaultErrorCodes.NotFound,
            "usernotinrole" => DefaultErrorCodes.BadRequest,

            null => DefaultErrorCodes.BadRequest,
            _ => DefaultErrorCodes.BadRequest,
        };
    }
}
