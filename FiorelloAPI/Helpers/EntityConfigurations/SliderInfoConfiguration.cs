using FiorelloAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiorelloAPI.Helpers.EntityConfigurations
{
    public class SliderInfoConfiguration : IEntityTypeConfiguration<SliderInfo>
    {
        public void Configure(EntityTypeBuilder<SliderInfo> builder)
        {
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
