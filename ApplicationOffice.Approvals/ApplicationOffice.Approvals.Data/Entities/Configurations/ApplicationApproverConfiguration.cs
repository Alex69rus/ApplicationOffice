using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationOffice.Approvals.Data.Entities.Configurations
{
    public class ApplicationApproverConfiguration : IEntityTypeConfiguration<ApplicationApprover>
    {
        public void Configure(EntityTypeBuilder<ApplicationApprover> builder)
        {
            builder.ToTable("application_approvers");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Application).WithMany(x => x.Approvers).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.User).WithMany(x => x.Assignees).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
