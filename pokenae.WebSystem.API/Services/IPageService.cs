using pokenae.WebSystem.Core.Entities;
using System.Threading.Tasks;

namespace pokenae.WebSystem.API.Services
{
    /// <summary>
    /// �y�[�W�T�[�r�X�̃C���^�[�t�F�[�X
    /// </summary>
    public interface IPageService
    {
        Task<M1Page> GetPageByRouteAsync(string route, string userId);
    }
}

