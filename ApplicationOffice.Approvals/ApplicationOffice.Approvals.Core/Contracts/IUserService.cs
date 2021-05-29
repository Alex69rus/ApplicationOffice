using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Contracts.Models;


namespace ApplicationOffice.Approvals.Core.Contracts
{
    public interface IUserService
    {
        Task<FullUserDto> GetUser(long userId);
    }
}