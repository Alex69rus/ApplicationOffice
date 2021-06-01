using System;
using ApplicationOffice.Web.UI.Approvals.Enums;

namespace ApplicationOffice.Web.UI.Approvals.Models
{
    public class FullApplicationApiModel
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime DueDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public ApplicationType Type { get; set; }
        public UserViewApiModel Author { get; set; } = default!;
        public ApplicationFieldApiModel[] Fields { get; set; } = default!;
    }
}
