using ApplicationOffice.Approvals.Core.Contracts.Enums;

namespace ApplicationOffice.Approvals.Core.Contracts.Models
{
    public record ApplicationApproverDto(
        UserViewDto Approver,
        ApplicationApproverStatus Status);
}
