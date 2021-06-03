using System;
using System.Threading.Tasks;
using ApplicationOffice.Sso.Core.Models;

namespace ApplicationOffice.Sso.Core.Services
{
    /// <summary>
    ///     User account management
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        ///     Add new user
        /// </summary>
        /// <param name="request">Request</param>
        Task Add(AddUserDto request);

        /// <summary>
        ///     Change user password
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="currentPassword">Current user password</param>
        /// <param name="newPassword">New user password</param>
        Task ChangePassword(string userId, string currentPassword, string newPassword);

        /// <summary>
        ///     Generate password reset token
        /// </summary>
        /// <param name="userId">User id for password reset</param>
        /// <returns>Reset token</returns>
        Task<string> GeneratePasswordResetToken(string userId);

        /// <summary>
        ///     Reset password by token
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="resetPassToken">Password reset token</param>
        /// <param name="newPassword">New password</param>
        Task ResetPassword(string userId, string resetPassToken, string newPassword);

        /// <summary>
        ///     Generates phone confirmation token.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Phone confirmation token and its expiration time.</returns>
        Task<(string token, DateTime tokenExpirationTime)> GenerateConfirmPhoneToken(string userId);

        /// <summary>
        ///     Confirms user phone.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="token">Confirmation token.</param>
        /// <returns>Confirmed phone number.</returns>
        Task<string> ConfirmPhone(string userId, string token);

        /// <summary>
        ///     Generates email confirmation token.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Email confirmation token and its expiration time.</returns>
        Task<(string token, DateTime tokenExpirationTime)> GenerateConfirmEmailToken(string userId);

        /// <summary>
        ///     Confirms user email.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="token">Confirmation token.</param>
        /// <returns>Confirmed email address.</returns>
        Task<string> ConfirmEmail(string userId, string token);
    }
}
