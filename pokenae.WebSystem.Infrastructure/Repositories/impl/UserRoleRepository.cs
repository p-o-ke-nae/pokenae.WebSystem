using Microsoft.EntityFrameworkCore;
using pokenae.WebSystem.Infrastructure.Data;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace pokenae.WebSystem.Infrastructure.Repositories.impl
{
    /// <summary>
    /// ���[�U�������|�W�g���̎���
    /// </summary>
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly WebSystemDbContext _context;

        public UserRoleRepository(WebSystemDbContext context)
        {
            _context = context;
        }

        public async Task<T1UserRole> GetUserRoleAsync(int userId)
        {
            return await _context.UserRoles
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.UserId == userId);
        }
    }
}

