using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Contracts.Enums;
using ApplicationOffice.Approvals.Core.Contracts.Models;

namespace ApplicationOffice.Approvals.Core.Contracts
{
    /// <summary>
    /// Application service
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Get all applications created by User
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="statuses">Application statuses for filtering</param>
        /// <returns>Applications</returns>
        Task<ApplicationViewDto[]> GetCreatedApplications(long userId, ApplicationStatus[]? statuses);

        /// <summary>
        /// Get all applications on User approval
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="applicationStatuses">Application statuses for filtering</param>
        /// <param name="approverStatuses">Application approver statuses for filtering</param>
        /// <returns>Applications</returns>
        Task<ApplicationViewDto[]> GetApprovalApplications(
            long userId,
            ApplicationStatus[]? applicationStatuses,
            ApplicationApproverStatus[]? approverStatuses);

        /// <summary>
        /// Get application by Id
        /// </summary>
        /// <param name="applicationId">Application id</param>
        /// <returns>Application</returns>
        Task<FullApplicationDto> Get(long applicationId);

        /// <summary>
        /// Create application
        /// </summary>
        /// <param name="request">Create application request</param>
        /// <returns>Created application id</returns>
        Task<long> CreateApplication(CreateApplicationRequestDto request);
    }
}
