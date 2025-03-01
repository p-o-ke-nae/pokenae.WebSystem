using pokenae.WebSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pokenae.WebSystem.Infrastructure.Repositories
{
    // ...existing code...
    public interface IPageRepository
    {
        Task<M1Page> GetPageByRouteAsync(string route);
        Task<IEnumerable<M1Page>> GetHeaderPagesAsync();
        Task<bool> CheckPageAccessAsync(string route, string userId);
    }
}

