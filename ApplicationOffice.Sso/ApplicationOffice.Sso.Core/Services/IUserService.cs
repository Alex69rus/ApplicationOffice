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
        /// <param name="userName">User name</param>
        /// <param name="currentPassword">Current user password</param>
        /// <param name="newPassword">New user password</param>
        Task ChangePassword(string userName, string currentPassword, string newPassword);

        /// <summary>
        ///     Generate password reset token
        /// </summary>
        /// <param name="userName">User name for password reset</param>
        /// <returns>Reset token</returns>
        Task<string> GeneratePasswordResetToken(string userName);

        /// <summary>
        ///     Reset password by token
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="resetPassToken">Password reset token</param>
        /// <param name="newPassword">New password</param>
        Task ResetPassword(string userName, string resetPassToken, string newPassword);

        /// <summary>
        ///     Generates phone confirmation token.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns>Phone confirmation token and its expiration time.</returns>
        Task<(string token, DateTime tokenExpirationTime)> GenerateConfirmPhoneToken(string userName);

        /// <summary>
        ///     Confirms user phone.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="token">Confirmation token.</param>
        /// <returns>Confirmed phone number.</returns>
        Task<string> ConfirmPhone(string userName, string token);

        /// <summary>
        ///     Generates email confirmation token.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns>Email confirmation token and its expiration time.</returns>
        Task<(string token, DateTime tokenExpirationTime)> GenerateConfirmEmailToken(string userName);

        /// <summary>
        ///     Confirms user email.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="token">Confirmation token.</param>
        /// <returns>Confirmed email address.</returns>
        Task<string> ConfirmEmail(string userName, string token);
    }
}
