using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Contracts.Enums;
using ApplicationOffice.Approvals.Core.Contracts.Models;

namespace ApplicationOffice.Approvals.Core.Contracts
{
    /// <summary>
    /// Application approver service
    /// </summary>
    public interface IApplicationApproverService
    {
        /// <summary>
        /// Get application approvers
        /// </summary>
        /// <param name="applicationId">Application id</param>
        /// <returns>Application approvers</returns>
        Task<ApplicationApproverDto[]> GetApplicationApprovers(long applicationId);

        /// <summary>
        /// Make decision about application approval
        /// </summary>
        /// <param name="approverId">Approver id</param>
        /// <param name="applicationId">Application id</param>
        /// <param name="status">Decision</param>
        Task MakeDecision(long approverId, long applicationId, ApplicationApproverStatus status);
    }
}
