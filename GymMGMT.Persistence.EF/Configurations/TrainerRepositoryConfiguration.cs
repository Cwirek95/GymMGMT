using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMGMT.Persistence.EF.Configurations
{
    public class TrainerRepositoryConfiguration : IRepositoryConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(128);
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


            builder.HasMany<Training>(tg => tg.Trainings)
                .WithOne(t => t.Trainer)
                .HasForeignKey(t => t.TrainerId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}