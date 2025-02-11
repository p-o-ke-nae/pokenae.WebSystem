using pokenae.WebSystem.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace pokenae.WebSystem.API.Services.impl
{
    /// <summary>
    /// �����`�F�b�N�̋��ʏ�����񋟂���T�[�r�X
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public AuthorizationService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// ���[�U�̌������`�F�b�N���܂�
        /// </summary>
        /// <param name="userId">���[�UID</param>
        /// <param name="requiredLevel">�K�v�Ȍ������x��</param>
        /// <returns>����������ꍇ��true�A�Ȃ��ꍇ��false</returns>
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

