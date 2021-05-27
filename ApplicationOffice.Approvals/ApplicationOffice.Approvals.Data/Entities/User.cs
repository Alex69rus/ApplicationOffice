using System;
using System.Collections.Generic;

namespace ApplicationOffice.Approvals.Data.Entities
{
    public class User : IEntity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public string Name { get; set; }
        public long UnitId { get; set; }
        public Unit Unit { get; set; } = default!;

        public ICollection<Application> CreatedApplications { get; protected set; }
        public ICollection<ApplicationApprover> Assignees { get; protected set; }

        public User(string name, long unitId)
        {
            CreatedAt = ModifiedAt = DateTime.UtcNow;

            Name = name;
            UnitId = unitId;

            CreatedApplications = new List<Application>();
            Assignees = new List<ApplicationApprover>();
        }
    }
}
