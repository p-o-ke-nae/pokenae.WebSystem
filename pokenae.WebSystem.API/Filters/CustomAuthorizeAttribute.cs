using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace pokenae.WebSystem.API.Filters
{
    /// <summary>
    /// カスタム認証フィルタ
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

                // 権限チェック
                var userHasPermission = CheckUserPermission(userId);
                if (!userHasPermission)
                {
                    context.Result = new ForbidResult("User does not have the required permissions.");
                }
            }

            private bool CheckUserPermission(string userId)
            {
                // ここに権限チェックのロジックを実装します
                // 例: データベースからユーザーの権限を取得してチェックする
                return true; // 仮の実装
            }
        }
    }
}
