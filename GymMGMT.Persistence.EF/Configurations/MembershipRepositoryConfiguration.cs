using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMGMT.Persistence.EF.Configurations
{
    public class MembershipRepositoryConfiguration : IRepositoryConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StartDate)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(x => x.LastExtension)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(x => x.EndDate)
                .IsRequired()
                .HasPrecision(0);
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


            builder.HasOne<Member>(ms => ms.Member)
                .WithOne(m => m.Membership)
                .HasForeignKey<Member>(m => m.MembershipId);
            builder.HasOne<MembershipType>(mt => mt.MembershipType)
                .WithMany(ms => ms.Memberships)
                .HasForeignKey(mt => mt.MembershipTypeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}