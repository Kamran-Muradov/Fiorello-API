using FiorelloAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiorelloAPI.Helpers.EntityConfigurations
{
    public class ExpertConfiguration : IEntityTypeConfiguration<Expert>
    {
        public void Configure(EntityTypeBuilder<Expert> builder)
        {
            builder.Property(m => m.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Position)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
