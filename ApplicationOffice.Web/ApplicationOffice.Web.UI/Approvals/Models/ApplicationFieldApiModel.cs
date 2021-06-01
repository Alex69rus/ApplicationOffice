using ApplicationOffice.Web.UI.Approvals.Enums;

namespace ApplicationOffice.Web.UI.Approvals.Models
{
    public class ApplicationFieldApiModel
    {
        public ApplicationFieldType Type { get; set; }
        public string Title { get; set; } = default!;
        public string? Value { get; set; }
    }
}
