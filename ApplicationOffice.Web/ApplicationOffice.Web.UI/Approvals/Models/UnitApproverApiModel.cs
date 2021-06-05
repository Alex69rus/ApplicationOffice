using ApplicationOffice.Web.UI.Approvals.Enums;

namespace ApplicationOffice.Web.UI.Approvals.Models
{
    public class UnitApproverApiModel
    {
        public long Id { get; set; }
        public string Title { get; set; } = default!;
        public long UnitId { get; set; }
        public UserViewApiModel Approver { get; set; } = default!;
        public ApplicationType ApplicationType { get; set; }
    }
}
