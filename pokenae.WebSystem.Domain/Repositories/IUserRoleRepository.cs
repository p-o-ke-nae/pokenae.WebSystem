using pokenae.WebSystem.Core.Entities;
using System.Threading.Tasks;

namespace pokenae.WebSystem.Infrastructure.Repositories
{
    /// <summary>
    /// ���[�U�������|�W�g���̃C���^�[�t�F�[�X
    /// </summary>
    public interface IUserRoleRepository
    {
        Task<T1UserRole> GetUserRoleAsync(string userId);
    }
}

