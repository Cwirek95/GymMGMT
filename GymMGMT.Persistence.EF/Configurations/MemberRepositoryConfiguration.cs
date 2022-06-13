using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMGMT.Persistence.EF.Configurations
{
    public class MemberRepositoryConfiguration : IRepositoryConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(x => x.DateOfBirth)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);
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



            builder.HasOne<Membership>(m => m.Membership)
                .WithOne(u => u.Member)
                .HasForeignKey<Membership>(u => u.MemberId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}