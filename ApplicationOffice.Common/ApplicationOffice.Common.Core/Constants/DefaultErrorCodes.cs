namespace ApplicationOffice.Common.Core.Constants
{
    /// <summary>
    /// Default error codes (range 1-999).
    /// </summary>
    public static class DefaultErrorCodes
    {
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int Forbidden = 403;
        public const int NotFound = 404;
        public const int Internal = 500;
    }
}