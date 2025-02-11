using pokenae.WebSystem.Core.Entities;
using System.Threading.Tasks;

namespace pokenae.WebSystem.API.Services
{
    /// <summary>
    /// ページサービスのインターフェース
    /// </summary>
    public interface IPageService
    {
        Task<M1Page> GetPageByRouteAsync(string route, string userId);
    }
}

