using System;
using ApplicationOffice.Approvals.Data.Enums;
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

            builder.HasData(UnitApproverSeed);
        }

        public static UnitApprover[] UnitApproverSeed
            => new[]
            {
                new UnitApprover(
                    1,
                    new(2021, 05, 01, 0, 0, 0, DateTimeKind.Utc),
                    "Руководитель отдела",
                    1,
                    1,
                    ApplicationType.RegularVacation),
                new UnitApprover(
                    2,
                    new(2021, 05, 01, 0, 0, 0, DateTimeKind.Utc),
                    "Отдел кадров",
                    1,
                    2,
                    ApplicationType.RegularVacation),
            };
    }
}
