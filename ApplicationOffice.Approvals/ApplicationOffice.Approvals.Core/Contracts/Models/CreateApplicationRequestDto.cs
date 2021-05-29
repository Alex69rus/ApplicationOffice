using System;
using ApplicationOffice.Approvals.Core.Contracts.Enums;

namespace ApplicationOffice.Approvals.Core.Contracts.Models
{
    public record CreateApplicationRequestDto(
        string Title,
        string Description,
        DateTime DueDate,
        ApplicationType Type,
        long AuthorId,
        ApplicationFieldDto[] Fields);
}
