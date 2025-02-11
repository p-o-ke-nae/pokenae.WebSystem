using System.Collections.Generic;
using System.Threading.Tasks;
using pokenae.WebSystem.Core.Entities;

namespace pokenae.WebSystem.Core.Repositories
{
    /// <summary>
    /// �J�e�S�����|�W�g���̃C���^�[�t�F�[�X
    /// </summary>
    public interface IPageCategoryRepository
    {
        Task<IEnumerable<M1PageCategory>> GetHeaderCategoriesAsync();
    }
}
