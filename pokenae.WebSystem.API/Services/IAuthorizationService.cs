using System.Threading.Tasks;

namespace pokenae.WebSystem.API.Services
{
    /// <summary>
    /// �����`�F�b�N�T�[�r�X�̃C���^�[�t�F�[�X
    /// </summary>
    public interface IAuthorizationService
    {
        Task<bool> CheckUserAuthorizationAsync(int userId, int requiredLevel);
    }
}

