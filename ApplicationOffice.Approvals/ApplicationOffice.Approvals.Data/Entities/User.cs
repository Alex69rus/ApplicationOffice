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
        public long? UnitId { get; set; }
        public Unit? Unit { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Application> CreatedApplications { get; protected set; }
        public ICollection<ApplicationApprover> Assignees { get; protected set; }

        internal User(long id, DateTime createdAt, string name, long? unitId, string email, DateTime birthDate)
        {
            Id = id;
            CreatedAt = ModifiedAt = createdAt;
            Name = name;
            UnitId = unitId;
            Email = email;
            BirthDate = birthDate;

            CreatedApplications = new List<Application>();
            Assignees = new List<ApplicationApprover>();
        }

        public User(string name, long? unitId, string email, DateTime birthDate)
        {
            CreatedAt = ModifiedAt = DateTime.UtcNow;

            Name = name;
            UnitId = unitId;
            Email = email;
            BirthDate = birthDate;
            
            CreatedApplications = new List<Application>();
            Assignees = new List<ApplicationApprover>();
        }
    }
}
