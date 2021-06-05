using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Contracts.Enums;
using ApplicationOffice.Approvals.Core.Contracts.Models;

namespace ApplicationOffice.Approvals.Core.Contracts
{
    public interface IUnitApproverService
    {
        Task<UnitApproverDto[]> GetUnitApprovers(long unitId, ApplicationType applicationType);
    }
}