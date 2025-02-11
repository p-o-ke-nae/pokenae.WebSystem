using Moq;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.Core.Services;
using pokenae.WebSystem.API.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pokenae.WebSystem.Tests.Controllers
{
    public class PageCategoryControllerTests
    {
        private readonly Mock<IPageCategoryService> _mockService;
        private readonly PageCategoryController _controller;

        public PageCategoryControllerTests()
        {
            _mockService = new Mock<IPageCategoryService>();
            _controller = new PageCategoryController(_mockService.Object);
        }

        [Fact]
        public async Task GetHeaderCategories_ReturnsOkResult_WithCategories()
        {
            // Arrange
            var categories = new List<M1PageCategory>
            {
                new M1PageCategory { Id = "1", Name = "Category1", DisplayOrder = 1, IsHeader = true },
                new M1PageCategory { Id = "2", Name = "Category2", DisplayOrder = 2, IsHeader = true }
            };
            _mockService.Setup(service => service.GetHeaderCategoriesAsync()).ReturnsAsync(categories);

            // Act
            var result = await _controller.GetHeaderCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCategories = Assert.IsType<List<M1PageCategory>>(okResult.Value);
            Assert.Equal(2, returnCategories.Count);
        }
    }
}
