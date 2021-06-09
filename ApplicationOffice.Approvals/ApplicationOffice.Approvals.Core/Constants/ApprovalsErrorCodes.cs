namespace ApplicationOffice.Approvals.Core.Constants
{
    /// <summary>
    /// Approvals error codes. Range(2000-2999)
    /// </summary>
    public class ApprovalsErrorCodes
    {
        public const int Internal = 2000;

        /// <summary>
        /// Application approval not found
        /// </summary>
        public const int ApprovalNotFound = 2001;

        /// <summary>
        /// Decision was already made
        /// </summary>
        public const int AlreadyMadeDecision = 2002;
        
        /// <summary>
        /// User not found
        /// </summary>
        public const int UserNotFound = 2003;

        /// <summary>
        /// User is not is a Unit
        /// </summary>
        public const int UserNotInUnit = 2004;

        /// <summary>
        /// Unit doesn't have configured approvers
        /// </summary>
        public const int UnitWithoutApprovers = 2005;

        /// <summary>
        /// Application not found
        /// </summary>
        public const int ApplicationNotFound = 2005;
    }
}