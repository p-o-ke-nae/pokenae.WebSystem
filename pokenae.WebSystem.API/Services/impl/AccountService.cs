using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using pokenae.WebSystem.API.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace pokenae.WebSystem.API.Services.impl
{
    /// <summary>
    /// アカウントサービスの実装
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<AuthenticationProperties> GetAuthenticationPropertiesAsync()
        {
            return Task.FromResult(new AuthenticationProperties { RedirectUri = "/api/account/google-callback" });
        }

        public async Task<IEnumerable<ClaimDto>> GetClaimsAsync()
        {
            var authenticateResult = await _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                return null;

            return authenticateResult.Principal.Identities
                .FirstOrDefault()?.Claims.Select(claim => new ClaimDto
                {
                    Type = claim.Type,
                    Value = claim.Value
                });
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
