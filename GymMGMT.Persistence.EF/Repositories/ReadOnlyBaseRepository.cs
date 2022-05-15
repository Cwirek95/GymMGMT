using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GymMGMT.Persistence.EF.Repositories
{
    public class ReadOnlyBaseRepository<TEntity> : IReadOnlyAsyncRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;

        public ReadOnlyBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            var entities = await _context.Set<TEntity>().ToListAsync();

            return entities;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, id);

            return entity;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, id);

            return entity;
        }
    }
}
