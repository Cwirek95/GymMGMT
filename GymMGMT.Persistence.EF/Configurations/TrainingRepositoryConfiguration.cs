using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMGMT.Persistence.EF.Configurations
{
    public class TrainingRepositoryConfiguration : IRepositoryConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StartDate)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(x => x.EndDate)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(x => x.Price)
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



            builder.HasMany(t => t.Members)
                .WithMany(m => m.Trainings);
        }
    }
}