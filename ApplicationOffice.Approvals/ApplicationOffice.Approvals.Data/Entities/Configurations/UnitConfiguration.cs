using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationOffice.Approvals.Data.Entities.Configurations
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("units");
            builder.HasKey(x => x.Id);

            builder.HasData(UnitSeed);
        }

        public static Unit[] UnitSeed
            => new[]
            {
                new Unit(1, new(2021, 05, 01, 0, 0, 0, DateTimeKind.Utc), "Отдел ИТ-разработки"),
            };
    }
}
