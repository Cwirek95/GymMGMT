using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMGMT.Persistence.EF.Configurations
{
    public class RoleRepositoryConfiguration : IRepositoryConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(x => x.Status)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
