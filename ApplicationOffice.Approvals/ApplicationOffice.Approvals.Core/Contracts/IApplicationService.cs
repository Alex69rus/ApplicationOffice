using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Contracts.Enums;
using ApplicationOffice.Approvals.Core.Contracts.Models;

namespace ApplicationOffice.Approvals.Core.Contracts
{
    public interface IApplicationService
    {
        Task<ApplicationViewDto[]> GetCreatedApplications(long userId, ApplicationStatus[]? statuses);

        Task<ApplicationViewDto[]> GetApprovalApplications(
            long userId,
            ApplicationStatus[]? applicationStatuses,
            ApplicationApproverStatus[]? approverStatuses);

        Task<FullApplicationDto> Get(long applicationId);
    }
}
