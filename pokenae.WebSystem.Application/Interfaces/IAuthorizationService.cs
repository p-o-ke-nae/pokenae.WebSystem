using System.Threading.Tasks;

namespace pokenae.WebSystem.Application.Interfaces
{
    /// <summary>
    /// �����`�F�b�N�T�[�r�X�̃C���^�[�t�F�[�X
    /// </summary>
    public interface IAuthorizationService
    {
        Task<bool> CheckUserAuthorizationAsync(string userId, int requiredLevel);
    }
}

