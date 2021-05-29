using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationOffice.Approvals.Data.Entities.Configurations
{
    public class UnitApproverConfiguration : IEntityTypeConfiguration<UnitApprover>
    {
        public void Configure(EntityTypeBuilder<UnitApprover> builder)
        {
            builder.ToTable("unit_approvers");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Approver);
            builder
                .HasOne(x => x.Unit)
                .WithMany(x => x.Approvers);
        }
    }
}
