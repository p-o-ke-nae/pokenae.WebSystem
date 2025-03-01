using pokenae.WebSystem.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace pokenae.WebSystem.Application.Interfaces
{
    // ...existing code...
    public interface IPageService
    {
        Task<M1Page> GetPageByRouteAsync(string route, string userId);
        Task<IEnumerable<M1Page>> GetHeaderPagesAsync();
        Task<bool> CheckPageAccessAsync(string route, string userId);
    }
}

