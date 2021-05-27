using System;
using ApplicationOffice.Approvals.Core.Contracts.Enums;

namespace ApplicationOffice.Approvals.Core.Contracts.Models
{
    public record ApplicationViewDto(
        long Id,
        DateTime CreatedAt,
        DateTime ModifiedAt,
        string Title,
        DateTime DueDate,
        ApplicationStatus Status);
}
