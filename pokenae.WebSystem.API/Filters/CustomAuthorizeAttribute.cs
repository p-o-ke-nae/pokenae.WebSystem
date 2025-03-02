using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using pokenae.Commons.Data;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.Infrastructure.Data;
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
            private readonly WebSystemDbContext _dbContext;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CustomAuthorizeFilter(WebSystemDbContext dbContext, IHttpContextAccessor httpContextAccessor)
            {
                _dbContext = dbContext;
                _httpContextAccessor = httpContextAccessor;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var user = context.HttpContext.User;
                if (user == null || !user.Identity?.IsAuthenticated == true)
                {
                    // �Q�X�g���[�U�Ƃ��Ĉ���
                    var isGuestAllowed = CheckGuestPermission(context);
                    if (isGuestAllowed)
                    {
                        return; // �Q�X�g���[�U��������Ă���ꍇ�͂��̂܂܏����𑱍s
                    }
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
                var requiredPermission = context.ActionDescriptor.EndpointMetadata
                    .OfType<PermissionAttribute>()
                    .FirstOrDefault()?.RequiredPermission;

                if (requiredPermission != null)
                {
                    var userHasPermission = CheckUserPermission(userId, context, requiredPermission.Value);
                    if (!userHasPermission)
                    {
                        context.Result = new ForbidResult("User does not have the required permissions.");
                    }
                }
            }

            private bool CheckGuestPermission(AuthorizationFilterContext context)
            {
                var nodeId = context.HttpContext.Request.Query["nodeId"].ToString();
                var page = _dbContext.Pages.FirstOrDefault(p => p.NodeID == nodeId);

                if (page == null)
                {
                    return false;
                }

                // �Q�X�g���[�U�͌��J����Ă���y�[�W�̂݉{���\
                return page.PageState == PageStates.Published;
            }

            private bool CheckUserPermission(string userId, AuthorizationFilterContext context, PermissionLevel requiredPermission)
            {
                var nodeId = context.HttpContext.Request.Query["nodeId"].ToString();

                var userPageAccess = _dbContext.UserPageAccesses
                    .FirstOrDefault(up => up.UserID == userId && up.NodeID == nodeId);

                if (userPageAccess == null)
                {
                    return false;
                }

                return userPageAccess.Permission >= requiredPermission;
            }
        }
    }
}
