using Microsoft.AspNetCore.Mvc;
using Moq;
using pokenae.WebSystem.API.Controllers;
using pokenae.WebSystem.API.Services;
using Xunit;
using System.Threading.Tasks;
using pokenae.WebSystem.API.DTOs;
using Microsoft.Extensions.Logging;

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
            _mockAccountService.Setup(service => service.GetClaimsAsync()).ReturnsAsync((IEnumerable<ClaimDto>)null);

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
