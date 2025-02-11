using System.Collections.Generic;
using System.Threading.Tasks;
using pokenae.WebSystem.Core.Entities;

namespace pokenae.WebSystem.Core.Services
{
    /// <summary>
    /// カテゴリサービスのインターフェース
    /// </summary>
    public interface IPageCategoryService
    {
        Task<IEnumerable<M1PageCategory>> GetHeaderCategoriesAsync();
    }
}
