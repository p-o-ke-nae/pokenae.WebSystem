using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using pokenae.WebSystem.API.DTOs;
using pokenae.WebSystem.API.Services;

[Route("api/[controller]")]
   [ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>
    /// Googleでログインします。
    /// </summary>
    [HttpGet("login")]
    public IActionResult Login()
    {
        var properties = new AuthenticationProperties { RedirectUri = "/api/account/google-callback" };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    /// <summary>
    /// Google認証のコールバックを処理します。
    /// </summary>
    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!authenticateResult.Succeeded)
            return BadRequest(); // 認証失敗

        var claims = authenticateResult.Principal.Identities
            .FirstOrDefault()?.Claims.Select(claim => new ClaimDto
            {
                Type = claim.Type,
                Value = claim.Value
            });

        return Redirect($"https://client.example.com/auth-callback?claims={System.Text.Json.JsonSerializer.Serialize(claims)}");
    }

    /// <summary>
    /// ログアウトします。
    /// </summary>
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }
}
   