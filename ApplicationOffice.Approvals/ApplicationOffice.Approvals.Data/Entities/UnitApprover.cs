using System;
using ApplicationOffice.Approvals.Data.Enums;

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
        public ApplicationType ApplicationType { get; protected set; }

        internal UnitApprover(
            long id,
            DateTime createdAt,
            string title,
            long unitId,
            long approverId,
            ApplicationType applicationType)
        {
            Id = id;
            CreatedAt = ModifiedAt = createdAt;
            Title = title;
            UnitId = unitId;
            ApproverId = approverId;
            ApplicationType = applicationType;
        }

        public UnitApprover(string title, long unitId, long approverId, ApplicationType applicationType)
        {
            CreatedAt = ModifiedAt = DateTime.UtcNow;
            Title = title;
            UnitId = unitId;
            ApproverId = approverId;
            ApplicationType = applicationType;
        }
    }
}
