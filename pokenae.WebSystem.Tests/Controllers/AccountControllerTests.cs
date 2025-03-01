using Microsoft.AspNetCore.Mvc;
using Moq;
using pokenae.WebSystem.API.Controllers;
using pokenae.WebSystem.API.Services;
using Xunit;
using System.Threading.Tasks;
using pokenae.WebSystem.API.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using pokenae.WebSystem.Application.Interfaces;

namespace pokenae.WebSystem.Tests.Controllers
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly Mock<ILogger<AccountController>> _mockLogger;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockLogger = new Mock<ILogger<AccountController>>();
            _controller = new AccountController(_mockAccountService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Login_ReturnsChallengeResult()
        {
            var result = _controller.Login();
            Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task GoogleCallback_ReturnsBadRequest_WhenAuthenticationFails()
        {
            // Arrange
            var authResult = AuthenticateResult.Fail("Authentication failed");
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(c => c.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme))
                           .ReturnsAsync(authResult);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            // Act
            var result = await _controller.GoogleCallback();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GoogleCallback_ReturnsRedirect_WhenAuthenticationSucceeds()
        {
            // Arrange
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Test User"),
                    new Claim(ClaimTypes.Email, "testuser@example.com")
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authResult = AuthenticateResult.Success(new AuthenticationTicket(principal, CookieAuthenticationDefaults.AuthenticationScheme));
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(c => c.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme))
                           .ReturnsAsync(authResult);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            // Act
            var result = await _controller.GoogleCallback();

            // Assert
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("http://pokenae.com", redirectResult.Url);
        }

        [Fact]
        public async Task Logout_ReturnsRedirectResult()
        {
            var result = await _controller.Logout();
            Assert.IsType<RedirectResult>(result);
        }
    }
}
