using ApplicationOffice.Approvals.Core.Contracts.Enums;

namespace ApplicationOffice.Approvals.Core.Contracts.Models
{
    public record ApplicationApproverDto(
        string Title,
        UserViewDto User,
        ApplicationApproverStatus Status);
}
