using System.Collections.Generic;
using System.Threading.Tasks;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.Core.Repositories;
using pokenae.WebSystem.Core.Services;

namespace pokenae.WebSystem.Core.Services.impl
{
    /// <summary>
    /// カテゴリサービスの実装
    /// </summary>
    public class PageCategoryService : IPageCategoryService
    {
        private readonly IPageCategoryRepository _repository;

        public PageCategoryService(IPageCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<M1PageCategory>> GetHeaderCategoriesAsync()
        {
            return await _repository.GetHeaderCategoriesAsync();
        }
    }
}
