using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMGMT.Persistence.EF.Configurations
{
    public class MembershipTypeRepositoryConfiguration : IRepositoryConfiguration<MembershipType>
    {
        public void Configure(EntityTypeBuilder<MembershipType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(x => x.DefaultPrice)
                .IsRequired();
            builder.Property(x => x.DurationInDays)
                .IsRequired();
            builder.Property(x => x.Status)
                .IsRequired()
                .HasDefaultValue(true);
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(x => x.CreatedBy)
                .IsRequired(false);
            builder.Property(x => x.LastModifiedAt)
                .HasPrecision(0);
            builder.Property(x => x.LastModifiedBy)
                .IsRequired(false);

            builder.HasMany<Membership>(mt => mt.Memberships)
                .WithOne(ms => ms.MembershipType)
                .HasForeignKey(ms => ms.MembershipTypeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}