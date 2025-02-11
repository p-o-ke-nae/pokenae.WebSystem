using pokenae.WebSystem.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace pokenae.WebSystem.API.Services.impl
{
    /// <summary>
    /// 権限チェックの共通処理を提供するサービス
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public AuthorizationService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// ユーザの権限をチェックします
        /// </summary>
        /// <param name="userId">ユーザID</param>
        /// <param name="requiredLevel">必要な権限レベル</param>
        /// <returns>権限がある場合はtrue、ない場合はfalse</returns>
        public async Task<bool> CheckUserAuthorizationAsync(string userId, int requiredLevel)
        {
            var userRole = await _userRoleRepository.GetUserRoleAsync(userId);

            if (userRole == null || userRole.Role.Level < requiredLevel)
            {
                return false;
            }

            return true;
        }
    }
}

