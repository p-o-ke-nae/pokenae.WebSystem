using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Moq;
using pokenae.WebSystem.API.DTOs;
using pokenae.WebSystem.API.Services.impl;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace pokenae.WebSystem.Tests.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly AccountService _service;

        public AccountServiceTests()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _service = new AccountService(_mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task GetAuthenticationPropertiesAsync_ReturnsProperties()
        {
            var result = await _service.GetAuthenticationPropertiesAsync();
            Assert.NotNull(result);
            Assert.Equal("/api/account/google-callback", result.RedirectUri);
        }

        [Fact]
        public async Task GetClaimsAsync_ReturnsClaims_WhenAuthenticationSucceeds()
        {
            // Arrange
            var claims = new List<Claim> { new Claim("type", "value") };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authResult = AuthenticateResult.Success(new AuthenticationTicket(principal, CookieAuthenticationDefaults.AuthenticationScheme));

            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme))
                .ReturnsAsync(authResult);

            // Act
            var result = await _service.GetClaimsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("type", result.First().Type);
            Assert.Equal("value", result.First().Value);
        }

        [Fact]
        public async Task SignOutAsync_CallsSignOut()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext).Returns(mockHttpContext.Object);

            // Act
            await _service.SignOutAsync();

            // Assert
            mockHttpContext.Verify(context => context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme), Times.Once);
        }
    }
}
