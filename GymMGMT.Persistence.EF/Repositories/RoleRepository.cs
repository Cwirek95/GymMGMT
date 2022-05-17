using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymMGMT.Persistence.EF.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Role>> GetAllWithDetailsAsync()
        {
            var roles = await _context.Roles
                .Include(x => x.Users).ToListAsync();

            return roles;
        }
    }
}
