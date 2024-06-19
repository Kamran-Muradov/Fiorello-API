using FiorelloAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiorelloAPI.Helpers.EntityConfigurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(m => m.Key)
                .IsRequired();

            builder.Property(m => m.Value)
                .IsRequired();
        }
    }
}
