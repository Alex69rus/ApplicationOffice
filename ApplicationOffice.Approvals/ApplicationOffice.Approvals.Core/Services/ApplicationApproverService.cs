using System.Linq;
using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Constants;
using ApplicationOffice.Approvals.Core.Contracts;
using ApplicationOffice.Approvals.Core.Contracts.Enums;
using ApplicationOffice.Approvals.Core.Contracts.Models;
using ApplicationOffice.Approvals.Data;
using ApplicationOffice.Common.Core.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ApplicationOffice.Approvals.Core.Services
{
    public class ApplicationApproverService : IApplicationApproverService
    {
        private readonly ApprovalsDbContext _dbContext;
        private readonly IMapper _mapper;

        public ApplicationApproverService(ApprovalsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApplicationApproverDto[]> GetApplicationApprovers(long applicationId)
        {
            return await _dbContext.ApplicationApprovers
                .Where(x => x.ApplicationId == applicationId)
                .ProjectTo<ApplicationApproverDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task MakeDecision(long approverId, long applicationId, ApplicationApproverStatus status)
        {
            var applicationApprovers = await _dbContext.ApplicationApprovers
                .Include(x => x.Application)
                .Where(x => x.ApplicationId == applicationId)
                .ToArrayAsync();
            var applicationApprover = applicationApprovers.FirstOrDefault(x => x.UserId == approverId);
            if (applicationApprover is null)
                throw new BadRequestException("Application approval not found", ApprovalsErrorCodes.ApprovalNotFound);

            if (applicationApprover.Status != Data.Enums.ApplicationApproverStatus.New)
                throw new BadRequestException("Decision was already made", ApprovalsErrorCodes.AlreadyMadeDecision);

            applicationApprover.SetStatus((Data.Enums.ApplicationApproverStatus) status);

            if (applicationApprovers.All(x => IsFinal(x.Status)))
            {
                if (applicationApprovers.Any(x => x.Status == Data.Enums.ApplicationApproverStatus.Rejected))
                    applicationApprover.Application.SetStatus(Data.Enums.ApplicationStatus.Rejected);
                else
                    applicationApprover.Application.SetStatus(Data.Enums.ApplicationStatus.Approved);
            }

            await _dbContext.SaveChangesAsync();
        }

        private static bool IsFinal(Data.Enums.ApplicationApproverStatus status)
            => status != Data.Enums.ApplicationApproverStatus.New;
    }
}
