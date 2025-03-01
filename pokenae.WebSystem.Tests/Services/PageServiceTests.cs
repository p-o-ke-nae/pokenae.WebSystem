using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.API.Services;
using pokenae.WebSystem.API.Services.impl;
using Xunit;
using pokenae.WebSystem.Infrastructure.Repositories;
using pokenae.WebSystem.Application.Interfaces;

namespace pokenae.WebSystem.Tests.Services
{
    public class PageServiceTests
    {
        private readonly Mock<IPageRepository> _mockRepository;
        private readonly IPageService _service;

        public PageServiceTests()
        {
            _mockRepository = new Mock<IPageRepository>();
            _service = new PageService(_mockRepository.Object, new AuthorizationService(new Mock<IUserRoleRepository>().Object));
        }

        [Fact]
        public async Task GetHeaderPagesAsync_ReturnsHeaderPagesInOrder()
        {
            // Arrange
            var pages = new List<M1Page>
                    {
                        new M1Page { NodeID = "1", Title = "Page1", DisplayOrder = 2, IsHeader = true, PageState = PageStates.Published },
                        new M1Page { NodeID = "2", Title = "Page2", DisplayOrder = 1, IsHeader = true, PageState = PageStates.Published }
                    };

            _mockRepository.Setup(repo => repo.GetHeaderPagesAsync())
                .ReturnsAsync(pages.OrderBy(p => p.DisplayOrder));

            // Act
            var result = await _service.GetHeaderPagesAsync();

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Equal("2", resultList[0].NodeID);
            Assert.Equal("1", resultList[1].NodeID);
        }

        [Fact]
        public async Task CheckPageAccessAsync_ReturnsTrue_ForPublishedPage()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.CheckPageAccessAsync("route1", "user1"))
                .ReturnsAsync(true);

            // Act
            var result = await _service.CheckPageAccessAsync("route1", "user1");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckPageAccessAsync_ReturnsFalse_ForUnpublishedPage()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.CheckPageAccessAsync("route3", "user1"))
                .ReturnsAsync(false);

            // Act
            var result = await _service.CheckPageAccessAsync("route3", "user1");

            // Assert
            Assert.False(result);
        }
    }
}
