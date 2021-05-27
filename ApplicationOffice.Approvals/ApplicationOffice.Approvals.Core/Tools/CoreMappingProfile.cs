using ApplicationOffice.Approvals.Core.Contracts.Models;
using ApplicationOffice.Approvals.Data.Entities;
using ApplicationOffice.Approvals.Data.Enums;
using AutoMapper;

namespace ApplicationOffice.Approvals.Core.Tools
{
    public class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            CreateMap<ApplicationStatus, Contracts.Enums.ApplicationStatus>()
                .ConvertUsing(x => (Contracts.Enums.ApplicationStatus) x);
            CreateMap<ApplicationType, Contracts.Enums.ApplicationType>()
                .ConvertUsing(x => (Contracts.Enums.ApplicationType) x);
            CreateMap<ApplicationApproverStatus, Contracts.Enums.ApplicationApproverStatus>()
                .ConvertUsing(x => (Contracts.Enums.ApplicationApproverStatus) x);
            CreateMap<ApplicationFieldType, Contracts.Enums.ApplicationFieldType>()
                .ConvertUsing(x => (Contracts.Enums.ApplicationFieldType) x);

            CreateMap<Application, ApplicationViewDto>();
            CreateMap<Application, FullApplicationDto>();
            CreateMap<ApplicationField, ApplicationFieldDto>();
            CreateMap<User, UserViewDto>();
        }
    }
}
