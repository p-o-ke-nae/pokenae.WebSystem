using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.Core.Repositories;
using pokenae.WebSystem.Infrastructure.Data;

namespace pokenae.WebSystem.Infrastructure.Repositories
{
    /// <summary>
    /// カテゴリリポジトリの実装
    /// </summary>
    public class PageCategoryRepository : IPageCategoryRepository
    {
        private readonly WebSystemDbContext _context;

        public PageCategoryRepository(WebSystemDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<M1PageCategory>> GetHeaderCategoriesAsync()
        {
            return await _context.PageCategories
                .Where(c => c.IsHeader)
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();
        }
    }
}
