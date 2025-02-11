using System.Collections.Generic;
using System.Threading.Tasks;
using pokenae.WebSystem.Core.Entities;

namespace pokenae.WebSystem.Core.Repositories
{
    /// <summary>
    /// カテゴリリポジトリのインターフェース
    /// </summary>
    public interface IPageCategoryRepository
    {
        Task<IEnumerable<M1PageCategory>> GetHeaderCategoriesAsync();
    }
}
