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
    /// Google�Ń��O�C�����܂��B
    /// </summary>
    [HttpGet("login")]
    public IActionResult Login()
    {
        var properties = new AuthenticationProperties { RedirectUri = "/api/account/google-callback" };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    /// <summary>
    /// Google�F�؂̃R�[���o�b�N���������܂��B
    /// </summary>
    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!authenticateResult.Succeeded)
            return BadRequest(); // �F�؎��s

        var claims = authenticateResult.Principal.Identities
            .FirstOrDefault()?.Claims.Select(claim => new ClaimDto
            {
                Type = claim.Type,
                Value = claim.Value
            });

        return Redirect($"https://client.example.com/auth-callback?claims={System.Text.Json.JsonSerializer.Serialize(claims)}");
    }

    /// <summary>
    /// ���O�A�E�g���܂��B
    /// </summary>
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }
}
   