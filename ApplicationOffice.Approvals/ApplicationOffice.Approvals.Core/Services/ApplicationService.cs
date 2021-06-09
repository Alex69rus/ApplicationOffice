using System.Linq;
using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Constants;
using ApplicationOffice.Approvals.Core.Contracts;
using ApplicationOffice.Approvals.Core.Contracts.Enums;
using ApplicationOffice.Approvals.Core.Contracts.Models;
using ApplicationOffice.Approvals.Data;
using ApplicationOffice.Approvals.Data.Entities;
using ApplicationOffice.Common.Core.Exceptions;
using ApplicationOffice.Common.Core.Extensions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ApplicationFieldType = ApplicationOffice.Approvals.Data.Enums.ApplicationFieldType;
using ApplicationType = ApplicationOffice.Approvals.Data.Enums.ApplicationType;

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
                ?? throw new NotFoundException("Application not found.", ApprovalsErrorCodes.ApplicationNotFound);
        }

        public async Task<long> CreateApplication(CreateApplicationRequestDto request)
        {
            var author = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == request.AuthorId);
            if (author is null)
                throw new BadRequestException("User not found", ApprovalsErrorCodes.UserNotFound);
            if (!author.UnitId.HasValue)
                throw new BadRequestException("User is not in a Unit", ApprovalsErrorCodes.UserNotInUnit);

            var unitApprovers = await _dbContext.UnitApprovers
                .Where(x => x.UnitId == author.UnitId && x.ApplicationType == (ApplicationType) request.Type)
                .ToArrayAsync();
            if (!unitApprovers.Any())
                throw new BadRequestException(
                    "Unit doesn't have configured approvers",
                    ApprovalsErrorCodes.UnitWithoutApprovers);

            var newApplication = new Application(
                request.Title,
                request.Description,
                request.DueDate,
                Data.Enums.ApplicationStatus.New,
                (ApplicationType) request.Type,
                request.AuthorId);
            newApplication.Approvers.Add(unitApprovers.Select(x =>
                new ApplicationApprover(x.Title, x.ApproverId, newApplication.Id)));
            newApplication.Fields.Add(request.Fields.Select(x =>
                new ApplicationField(newApplication.Id, (ApplicationFieldType) x.Type, x.Title, x.Value)));

            _dbContext.Add(newApplication);
            await _dbContext.SaveChangesAsync();

            return newApplication.Id;
        }
    }
}
