using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymMGMT.Persistence.EF.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<User>> GetAllWithDetailsAsync()
        {
            var users = await _context.Users
                .Include(x => x.Role).ToListAsync();

            return users;
        }

        public async Task<User> GetByIdWithDetailsAsync(Guid id)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<User> GetByEmailWithDetailsAsync(string email)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }
    }
}
