using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMGMT.Persistence.EF.Configurations
{
    public interface IRepositoryConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        new void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
