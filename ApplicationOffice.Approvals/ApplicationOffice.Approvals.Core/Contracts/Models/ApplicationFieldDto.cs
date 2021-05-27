using ApplicationOffice.Approvals.Core.Contracts.Enums;

namespace ApplicationOffice.Approvals.Core.Contracts.Models
{
    public record ApplicationFieldDto(
        ApplicationFieldType Type,
        string Title,
        string? Value);
}
