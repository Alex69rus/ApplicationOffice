using ApplicationOffice.Approvals.Core.Contracts.Models;

namespace ApplicationOffice.Approvals.Core.Contracts
{
    public interface IApplicationApproverService
    {
        ApplicationApproverDto[] GetApplicationApprovers(long applicationId);
    }
}
