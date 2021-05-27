using System;
using ApplicationOffice.Approvals.Core.Contracts.Enums;

namespace ApplicationOffice.Approvals.Core.Contracts.Models
{
    public record FullApplicationDto(
        long Id,
        DateTime CreatedAt,
        DateTime ModifiedAt,
        string Title,
        string Description,
        DateTime DueDate,
        ApplicationStatus Status,
        ApplicationType Type,
        UserViewDto Author,
        ApplicationFieldDto[] Fields);
}
