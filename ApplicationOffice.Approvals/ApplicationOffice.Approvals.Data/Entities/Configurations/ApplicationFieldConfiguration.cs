using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationOffice.Approvals.Data.Entities.Configurations
{
    public class ApplicationFieldConfiguration : IEntityTypeConfiguration<ApplicationField>
    {
        public void Configure(EntityTypeBuilder<ApplicationField> builder)
        {
            builder.ToTable("application_fields");
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Application)
                .WithMany(x => x.Fields);
        }
    }
}
