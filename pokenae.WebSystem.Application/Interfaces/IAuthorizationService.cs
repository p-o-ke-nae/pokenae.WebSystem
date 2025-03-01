using System.Threading.Tasks;

namespace pokenae.WebSystem.Application.Interfaces
{
    /// <summary>
    /// 権限チェックサービスのインターフェース
    /// </summary>
    public interface IAuthorizationService
    {
        Task<bool> CheckUserAuthorizationAsync(string userId, int requiredLevel);
    }
}

