using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationOffice.Approvals.Data.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Unit).WithMany(x => x!.Employees).IsRequired(false);

            builder.HasData(UserSeed);
        }

        public static User[] UserSeed
            => new[]
            {
                new User(1, new(2021, 05, 01, 0, 0, 0, DateTimeKind.Utc), "Петров Пётр Петрович", 1),
                new User(2, new(2021, 05, 01, 0, 0, 0, DateTimeKind.Utc), "Иванов Иван Иванович", 1),
                new User(3, new(2021, 05, 01, 0, 0, 0, DateTimeKind.Utc), "Алексеев Алексей Алексеевич", 1),
            };
    }
}
