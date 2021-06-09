namespace ApplicationOffice.Sso.Core.Constants
{
    /// <summary>
    /// Sso error codes. Range(1000-1999)
    /// </summary>
    public static class SsoErrorCodes
    {
        public const int Internal = 1000;

        /// <summary>
        /// User account already exists
        /// </summary>
        public const int UserAccountAlreadyExist = 1001;

        /// <summary>
        /// Token creation attempts exceeded
        /// </summary>
        public const int FrequentTokenCreation = 1002;

        /// <summary>
        /// Not enough time has passed since the last attempt.
        /// </summary>
        public const int FrequentTokenValidation = 1003;

        /// <summary>
        /// Daily token validation attempts exceeded
        /// </summary>
        public const int DailyFrequentTokenValidation = 1004;

        /// <summary>
        /// User cannot reuse N old passwords
        /// </summary>
        public const int OldPasswordUsage = 1005;

        /// <summary>
        /// User account not found
        /// </summary>
        public const int UserAccountNotFound = 1006;

        /// <summary>
        /// Ivalid password reset token
        /// </summary>
        public const int InvalidPasswordResetToken = 1007;

        /// <summary>
        /// Invalid password
        /// </summary>
        public const int InvalidPassword = 1008;

        /// <summary>
        /// Password must have at least 1 digit
        /// </summary>
        public const int PasswordRequiresDigit = 1009;

        /// <summary>
        /// Password must have at least 1 lower letter
        /// </summary>
        public const int PasswordRequiresLower= 1010;

        /// <summary>
        /// Password must have at least 1 non letter or digit symbol
        /// </summary>
        public const int PasswordRequiresNonAlphanumeric = 1011;

        /// <summary>
        /// Password must have at leat 1 upper letter
        /// </summary>
        public const int PasswordRequiresUpper = 1012;

        /// <summary>
        /// Password too short
        /// </summary>
        public const int PasswordTooShort = 1013;

        /// <summary>
        /// Password too long
        /// </summary>
        public const int PasswordTooLong = 1014;

        /// <summary>
        /// After success verification token usage token that was used not found
        /// </summary>
        public const int TokenToDeactivateNotFound = 1015;
    }
}