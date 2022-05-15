using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymMGMT.Persistence.EF.Repositories
{
    public class UserRepository : ReadOnlyBaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                throw new NotFoundException(nameof(User), user.Id);

            return user;
        }
    }
}
