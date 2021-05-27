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
        public long ManagerId { get; protected set; }
        public User Manager { get; protected set; } = default!;
        public long HrManagerId { get; protected set; }
        public User HrManager { get; protected set; } = default!;

        public ICollection<User> Employee { get; set; }

        public Unit(string title, long managerId, long hrManagerId)
        {
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;

            Title = title;
            ManagerId = managerId;
            HrManagerId = hrManagerId;

            Employee = new List<User>();
        }
    }
}
