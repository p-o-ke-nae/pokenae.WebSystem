using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace pokenae.WebSystem.API.Filters
{
    /// <summary>
    /// �J�X�^���F�؃t�B���^
    /// </summary>
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute() : base(typeof(CustomAuthorizeFilter))
        {
        }

        private class CustomAuthorizeFilter : IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var user = context.HttpContext.User;
                if (user == null || !user.Identity.IsAuthenticated)
                {
                    context.Result = new UnauthorizedObjectResult(new { message = "User is not authenticated." });
                    return;
                }

                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    context.Result = new UnauthorizedObjectResult(new { message = "User is not authenticated." });
                    return;
                }

                // �����`�F�b�N
                var userHasPermission = CheckUserPermission(userId);
                if (!userHasPermission)
                {
                    context.Result = new ForbidResult("User does not have the required permissions.");
                }
            }

            private bool CheckUserPermission(string userId)
            {
                // �����Ɍ����`�F�b�N�̃��W�b�N���������܂�
                // ��: �f�[�^�x�[�X���烆�[�U�[�̌������擾���ă`�F�b�N����
                return true; // ���̎���
            }
        }
    }
}
