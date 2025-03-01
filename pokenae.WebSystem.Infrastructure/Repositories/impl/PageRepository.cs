using Microsoft.EntityFrameworkCore;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.Infrastructure.Data;
using pokenae.WebSystem.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
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
            var page = await _context.Pages.FirstOrDefaultAsync(p => p.Route == route);
            if (page == null)
            {
                throw new KeyNotFoundException("The page you are looking for does not exist.");
            }
            return page;
        }

        public async Task<IEnumerable<M1Page>> GetHeaderPagesAsync()
        {
            return await _context.Pages
                .Where(p => p.IsHeader)
                .OrderBy(p => p.DisplayOrder)
                .ToListAsync();
        }

        public async Task<bool> CheckPageAccessAsync(string route, string userId)
        {
            var page = await _context.Pages.FirstOrDefaultAsync(p => p.Route == route);
            if (page == null)
            {
                return false;
            }

            if (page.PageState == PageStates.Published)
            {
                return true;
            }

            if (page.PageState == PageStates.LimitedAccess)
            {
                // Check user access from the junction table
                var userPageAccess = await _context.UserPageAccesses
                    .FirstOrDefaultAsync(upa => upa.NodeID == page.NodeID && upa.UserID == userId);
                return userPageAccess != null;
            }

            return false;
        }
    }
}

