namespace ApplicationOffice.Approvals.Core.Contracts.Models
{
    public record FullUserDto(
        long Id,
        string Name,
        long? UnitId,
        string? UnitTitle);
}