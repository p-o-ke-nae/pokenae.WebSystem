using Microsoft.AspNetCore.Authentication;
using pokenae.WebSystem.API.DTOs;

namespace pokenae.WebSystem.API.Services
{
    /// <summary>
    /// アカウントサービスのインターフェース
    /// </summary>
    public interface IAccountService
    {
        Task<AuthenticationProperties> GetAuthenticationPropertiesAsync();
        Task<IEnumerable<ClaimDto>> GetClaimsAsync();
        Task SignOutAsync();
    }
}
