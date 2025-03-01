using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pokenae.WebSystem.API.DTOs;
using pokenae.WebSystem.API.Services;
using pokenae.WebSystem.Application.Interfaces;

[Route("api/[controller]")]
   [ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAccountService accountService, ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    /// <summary>
    /// Googleでログインします。
    /// </summary>
    [HttpGet("login")]
    public IActionResult Login()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("GoogleCallback", "Account"),
            Items = { { "LoginProvider", "Google" } },
            AllowRefresh = true, // リフレッシュを許可
            //Parameters = { { "state", Guid.NewGuid().ToString("N") } } // state パラメータを追加
        };
        _logger.LogInformation("Redirecting to Google for authentication.");
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    /// <summary>
    /// Google認証のコールバックを処理します。
    /// </summary>
    [HttpGet("google-callback")]
    [AllowAnonymous]
    public async Task<IActionResult> GoogleCallback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!authenticateResult.Succeeded)
        {
            _logger.LogError("Google authentication failed.");
            return BadRequest("Google authentication failed.");
        }

        var claims = authenticateResult.Principal.Identities
            .FirstOrDefault()?.Claims.Select(claim => new ClaimDto
            {
                Type = claim.Type,
                Value = claim.Value
            });

        _logger.LogInformation("Google authentication succeeded.");
        ////return Redirect($"https://client.example.com/auth-callback?claims={System.Text.Json.JsonSerializer.Serialize(claims)}");
        return Redirect($"http://pokenae.com");
    }

    /// <summary>
    /// ログアウトします。
    /// </summary>
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        _logger.LogInformation("User logged out.");
        return Redirect("http://pokenae.com");
    }
}
   