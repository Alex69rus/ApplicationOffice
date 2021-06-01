using System;
using ApplicationOffice.Web.UI.Approvals.Enums;

namespace ApplicationOffice.Web.UI.Tools
{
    public static class ApplicationStatusExtensions
    {
        public static bool IsFinal(this ApplicationStatus status)
            => status switch
            {
                ApplicationStatus.New => false,
                ApplicationStatus.Approved => true,
                ApplicationStatus.Rejected => true,

                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
    }
}
