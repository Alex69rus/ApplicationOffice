using ApplicationOffice.Web.UI.Approvals.Enums;

namespace ApplicationOffice.Web.UI.Approvals.Models
{
    public class ApplicationApproverApiModel
    {
        public string Title { get; set; } = default!;
        public UserViewApiModel User { get; set; } = default!;
        public ApplicationApproverStatus Status { get; set; }
    }
}
