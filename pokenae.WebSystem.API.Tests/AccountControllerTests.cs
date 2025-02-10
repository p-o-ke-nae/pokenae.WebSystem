using Microsoft.AspNetCore.Mvc;
using Moq;
using pokenae.WebSystem.API.Controllers;
using pokenae.WebSystem.API.Services;
using Xunit;

namespace pokenae.WebSystem.API.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _controller = new AccountController(_mockAccountService.Object);
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
            // HttpContextのモックを設定して認証失敗をシミュレート

            // Act
            var result = await _controller.GoogleCallback();

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Logout_ReturnsRedirectResult()
        {
            var result = await _controller.Logout();
            Assert.IsType<RedirectResult>(result);
        }
    }
}
