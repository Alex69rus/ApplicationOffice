using System;
using ApplicationOffice.Approvals.Data.Enums;

namespace ApplicationOffice.Approvals.Data.Entities
{
    public class ApplicationField : IEntity
    {
        public long Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime ModifiedAt { get; protected set; }

        public long ApplicationId { get; protected set; }
        public Application Application { get; protected set; } = default!;
        public ApplicationFieldType Type { get; protected set; }
        public string Title { get; protected set; }
        public string? Value { get; protected set; }

        public ApplicationField(long applicationId, ApplicationFieldType type, string title, string? value)
        {
            CreatedAt = ModifiedAt = DateTime.UtcNow;

            ApplicationId = applicationId;
            Type = type;
            Title = title;
            Value = value;
        }
    }
}
