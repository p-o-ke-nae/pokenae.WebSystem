using pokenae.WebSystem.Core.Entities;
using System.Threading.Tasks;

namespace pokenae.WebSystem.Infrastructure.Repositories
{
    /// <summary>
    /// �y�[�W���|�W�g���̃C���^�[�t�F�[�X
    /// </summary>
    public interface IPageRepository
    {
        Task<M1Page> GetPageByRouteAsync(string route);
    }
}

