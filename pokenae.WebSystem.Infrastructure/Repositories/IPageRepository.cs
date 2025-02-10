using pokenae.WebSystem.Core.Entities;
using System.Threading.Tasks;

namespace pokenae.WebSystem.Infrastructure.Repositories
{
    /// <summary>
    /// ページリポジトリのインターフェース
    /// </summary>
    public interface IPageRepository
    {
        Task<M1Page> GetPageByRouteAsync(string route);
    }
}

