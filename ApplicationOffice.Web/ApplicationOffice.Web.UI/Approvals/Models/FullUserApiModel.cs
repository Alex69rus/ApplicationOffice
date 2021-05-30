namespace ApplicationOffice.Web.UI.Approvals.Models
{
    public class FullUserApiModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public long? UnitId { get; set; }
        public string? UnitName { get; set; }
    }
}
