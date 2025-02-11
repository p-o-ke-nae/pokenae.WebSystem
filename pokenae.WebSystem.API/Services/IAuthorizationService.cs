using System.Threading.Tasks;

namespace pokenae.WebSystem.API.Services
{
    /// <summary>
    /// 権限チェックサービスのインターフェース
    /// </summary>
    public interface IAuthorizationService
    {
        Task<bool> CheckUserAuthorizationAsync(string userId, int requiredLevel);
    }
}

