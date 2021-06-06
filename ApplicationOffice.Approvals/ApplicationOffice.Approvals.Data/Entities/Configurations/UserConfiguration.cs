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
                new User(
                    1,
                    new(2021, 05, 01, 0, 0, 0, DateTimeKind.Utc),
                    "Петров Пётр Петрович",
                    1,
                    "PP.Petrov@mail.ru",
                    new(1970, 07, 1, 0, 0, 0, DateTimeKind.Utc)),
                new User(
                    2,
                    new(2021, 05, 01, 0, 0, 0, DateTimeKind.Utc),
                    "Иванов Иван Иванович",
                    1,
                    "II.Ivanov@mail.ru",
                    new(1987, 02, 10, 0, 0, 0, DateTimeKind.Utc)),
                new User(
                    3,
                    new(2021, 05, 01, 0, 0, 0, DateTimeKind.Utc),
                    "Алексеев Алексей Алексеевич",
                    1,
                    "AA.Alexeev@mail.ru",
                    new(1990, 09, 27, 0, 0, 0, DateTimeKind.Utc)),
            };
    }
}
