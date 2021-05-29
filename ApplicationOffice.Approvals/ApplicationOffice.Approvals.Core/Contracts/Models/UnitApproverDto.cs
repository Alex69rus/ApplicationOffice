namespace ApplicationOffice.Approvals.Core.Contracts.Models
{
    public record UnitApproverDto(
        long Id,
        string Title,
        long UnitId,
        UserViewDto Approver);
}