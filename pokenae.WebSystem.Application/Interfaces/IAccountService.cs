using Microsoft.AspNetCore.Authentication;
using pokenae.WebSystem.API.DTOs;

namespace pokenae.WebSystem.Application.Interfaces
{
    /// <summary>
    /// �A�J�E���g�T�[�r�X�̃C���^�[�t�F�[�X
    /// </summary>
    public interface IAccountService
    {
        Task<AuthenticationProperties> GetAuthenticationPropertiesAsync();
        Task<IEnumerable<ClaimDto>> GetClaimsAsync();
        Task SignOutAsync();
    }
}
