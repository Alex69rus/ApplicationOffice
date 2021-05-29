using System;

namespace ApplicationOffice.Approvals.Data.Entities
{
    public class UnitApprover : IEntity
    {
        public long Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime ModifiedAt { get; protected set; }

        public string Title { get; protected set; }
        public long UnitId { get; protected set; }
        public Unit Unit { get; protected set; } = default!;
        public long ApproverId { get; protected set; }
        public User Approver { get; protected set; } = default!;

        public UnitApprover(string title, long unitId, long approverId)
        {
            CreatedAt = ModifiedAt = DateTime.UtcNow;
            Title = title;
            UnitId = unitId;
            ApproverId = approverId;
        }
    }
}
