using System;
using System.Collections.Generic;

namespace ApplicationOffice.Approvals.Data.Entities
{
    public class Unit : IEntity
    {
        public long Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime ModifiedAt { get; protected set; }

        public string Title { get; protected set; }

        public ICollection<User> Employees { get; protected set; }
        public ICollection<UnitApprover> Approvers { get; protected set; }

        public Unit(string title)
        {
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;

            Title = title;

            Employees = new List<User>();
            Approvers = new List<UnitApprover>();
        }
    }
}
