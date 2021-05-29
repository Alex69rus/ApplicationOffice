using System;
using System.Collections.Generic;
using ApplicationOffice.Approvals.Data.Enums;

namespace ApplicationOffice.Approvals.Data.Entities
{
    public class Application : IEntity
    {
        public long Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime ModifiedAt { get; protected set; }

        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public DateTime DueDate { get; protected set; }
        public ApplicationStatus Status { get; protected set; }
        public ApplicationType Type { get; protected set; }

        public long AuthorId { get; protected set; }
        public User Author { get; protected set; } = default!;

        public ICollection<ApplicationApprover> Approvers { get; protected set; }
        public ICollection<ApplicationField> Fields { get; protected set; }

        public Application(
            string title,
            string description,
            DateTime dueDate,
            ApplicationStatus status,
            ApplicationType type,
            long authorId)
        {
            CreatedAt = ModifiedAt = DateTime.UtcNow;

            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = status;
            Type = type;
            AuthorId = authorId;

            Approvers = new List<ApplicationApprover>();
            Fields = new List<ApplicationField>();
        }

        public void SetStatus(ApplicationStatus status)
        {
            Status = status;
        }
    }
}
