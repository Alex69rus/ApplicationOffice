using System.Linq;
using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Contracts;
using ApplicationOffice.Approvals.Core.Contracts.Enums;
using ApplicationOffice.Approvals.Core.Contracts.Models;
using ApplicationOffice.Approvals.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ApplicationOffice.Approvals.Core.Services
{
    public class UnitApproverService : IUnitApproverService
    {
        private readonly ApprovalsDbContext _dbContext;
        private readonly IMapper _mapper;

        public UnitApproverService(ApprovalsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UnitApproverDto[]> GetUnitApprovers(long unitId, ApplicationType applicationType)
        {
            return await _dbContext.UnitApprovers
                .Where(x => x.UnitId == unitId && x.ApplicationType == (Data.Enums.ApplicationType) applicationType)
                .ProjectTo<UnitApproverDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}