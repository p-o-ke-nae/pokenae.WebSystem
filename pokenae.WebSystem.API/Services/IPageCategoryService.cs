using System.Collections.Generic;
using System.Threading.Tasks;
using pokenae.WebSystem.Core.Entities;

namespace pokenae.WebSystem.API.Services
{
    /// <summary>
    /// �J�e�S���T�[�r�X�̃C���^�[�t�F�[�X
    /// </summary>
    public interface IPageCategoryService
    {
        Task<IEnumerable<M1PageCategory>> GetHeaderCategoriesAsync();
    }
}
