using System;

namespace ApplicationOffice.Approvals.Data.Entities
{
    public interface IEntity
    {
        public long Id { get; }
        public DateTime CreatedAt { get; }
        public DateTime ModifiedAt { get; }
    }
}
