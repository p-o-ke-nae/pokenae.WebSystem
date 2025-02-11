using pokenae.WebSystem.Core.Entities;
using System.Threading.Tasks;

namespace pokenae.WebSystem.Infrastructure.Repositories
{
    /// <summary>
    /// ユーザ権限リポジトリのインターフェース
    /// </summary>
    public interface IUserRoleRepository
    {
        Task<T1UserRole> GetUserRoleAsync(string userId);
    }
}

