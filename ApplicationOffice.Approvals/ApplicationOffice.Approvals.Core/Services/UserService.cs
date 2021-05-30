using System.Linq;
using System.Threading.Tasks;
using ApplicationOffice.Approvals.Core.Contracts;
using ApplicationOffice.Approvals.Core.Contracts.Models;
using ApplicationOffice.Approvals.Data;
using ApplicationOffice.Common.Core.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ApplicationOffice.Approvals.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApprovalsDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(ApprovalsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<FullUserDto> GetUser(long userId)
        {
            return await _dbContext.Users
                .Where(x => x.Id == userId)
                .ProjectTo<FullUserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                ?? throw new NotFoundException("User not found");
        }
    }
}