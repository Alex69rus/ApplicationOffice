using System;
using ApplicationOffice.Approvals.Data.Enums;

namespace ApplicationOffice.Approvals.Data.Entities
{
    public class ApplicationApprover : IEntity
    {
        public long Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime ModifiedAt { get; protected set; }

        public long UserId { get; protected set; }
        public User User { get; protected set; } = default!;
        public long ApplicationId { get; protected set; }
        public Application Application { get; protected set; } = default!;
        public ApplicationApproverStatus Status { get; protected set; }

        public ApplicationApprover(long userId, long applicationId)
        {
            CreatedAt = ModifiedAt = DateTime.UtcNow;

            UserId = userId;
            ApplicationId = applicationId;
            Status = ApplicationApproverStatus.New;
        }
    }
}
