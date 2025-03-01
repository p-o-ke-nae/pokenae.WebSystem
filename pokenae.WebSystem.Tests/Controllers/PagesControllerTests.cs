using Moq;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.API.Services;
using pokenae.WebSystem.API.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pokenae.WebSystem.API.DTOs;
using pokenae.WebSystem.Application.Interfaces;

namespace pokenae.WebSystem.Tests.Controllers
{
    public class PagesControllerTests
    {
        private readonly Mock<IPageService> _mockService;
        private readonly PagesController _controller;

        public PagesControllerTests()
        {
            _mockService = new Mock<IPageService>();
            _controller = new PagesController(_mockService.Object);
        }

        [Fact]
        public async Task GetHeaderPages_ReturnsOkResult_WithPages()
        {
            // Arrange
            var pages = new List<M1Page>
                {
                    new M1Page { NodeID = "1", Title = "Page1", DisplayOrder = 1, IsHeader = true, PageState = PageStates.Published },
                    new M1Page { NodeID = "2", Title = "Page2", DisplayOrder = 2, IsHeader = true, PageState = PageStates.Published }
                };
            _mockService.Setup(service => service.GetHeaderPagesAsync()).ReturnsAsync(pages);

            // Act
            var result = await _controller.GetHeaderPages();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnPages = Assert.IsType<List<PageDto>>(okResult.Value);
            Assert.Equal(2, returnPages.Count);
        }

        [Fact]
        public async Task CheckPageAccess_ReturnsOkResult_ForPublishedPage()
        {
            // Arrange
            _mockService.Setup(service => service.CheckPageAccessAsync("route1", "user1"))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.CheckPageAccess("route1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnMessage = Assert.IsType<Dictionary<string, string>>(okResult.Value);
            Assert.Equal("User has access to this page.", returnMessage["message"]);
        }

        [Fact]
        public async Task CheckPageAccess_ReturnsForbidResult_ForUnpublishedPage()
        {
            // Arrange
            _mockService.Setup(service => service.CheckPageAccessAsync("route3", "user1"))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.CheckPageAccess("route3");

            // Assert
            var forbidResult = Assert.IsType<ForbidResult>(result);
        }
    }
}
