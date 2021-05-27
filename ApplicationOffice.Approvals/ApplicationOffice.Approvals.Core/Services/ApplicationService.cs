using System.Linq;
using System.Threading.Tasks;
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
    public class ApplicationService : IApplicationService
    {
        private readonly ApprovalsDbContext _dbContext;
        private readonly IMapper _mapper;

        public ApplicationService(ApprovalsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApplicationViewDto[]> GetCreatedApplications(long userId, ApplicationStatus[]? statuses)
        {
            var query = _dbContext.Applications.Where(x => x.AuthorId == userId);
            if (statuses?.Any() == true)
                query = query.Where(x => statuses.Cast<Data.Enums.ApplicationStatus>().ToArray().Contains(x.Status));

            return await query
                .OrderBy(x => x.DueDate)
                .ProjectTo<ApplicationViewDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task<ApplicationViewDto[]> GetApprovalApplications(
            long userId,
            ApplicationStatus[]? applicationStatuses,
            ApplicationApproverStatus[]? approverStatuses)
        {
            var query = _dbContext.ApplicationApprovers.Where(x => x.UserId == userId);
            if (applicationStatuses?.Any() == true)
                query = query.Where(x =>
                    applicationStatuses.Cast<Data.Enums.ApplicationStatus>().ToArray().Contains(x.Application.Status));
            if (approverStatuses?.Any() == true)
                query = query.Where(x =>
                    approverStatuses.Cast<Data.Enums.ApplicationApproverStatus>().ToArray().Contains(x.Status));

            return await query
                .Select(x => x.Application)
                .OrderBy(x => x.DueDate)
                .ProjectTo<ApplicationViewDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task<FullApplicationDto> Get(long applicationId)
        {
            return await _dbContext.Applications
                    .Where(x => x.Id == applicationId)
                    .ProjectTo<FullApplicationDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
                ?? throw new NotFoundException("Application not found.");
        }
    }
}
