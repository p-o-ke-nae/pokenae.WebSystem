using Microsoft.EntityFrameworkCore;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.Infrastructure.Data;
using pokenae.WebSystem.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace pokenae.WebSystem.Infrastructure.Repositories.impl
{
    /// <summary>
    /// ページリポジトリの実装
    /// </summary>
    public class PageRepository : IPageRepository
    {
        private readonly WebSystemDbContext _context;

        public PageRepository(WebSystemDbContext context)
        {
            _context = context;
        }

        public async Task<M1Page> GetPageByRouteAsync(string route)
        {
            return await _context.Pages.FirstOrDefaultAsync(p => p.Route == route);
        }
    }
}

