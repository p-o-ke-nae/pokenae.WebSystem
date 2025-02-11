using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.Core.Repositories;
using pokenae.WebSystem.API.Services;
using pokenae.WebSystem.API.Services.impl;
using Xunit;

namespace pokenae.WebSystem.Tests.Services
{
    public class PageCategoryServiceTests
    {
        private readonly Mock<IPageCategoryRepository> _mockRepository;
        private readonly IPageCategoryService _service;

        public PageCategoryServiceTests()
        {
            _mockRepository = new Mock<IPageCategoryRepository>();
            _service = new PageCategoryService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetHeaderCategoriesAsync_ReturnsHeaderCategoriesInOrder()
        {
            // Arrange
            var categories = new List<M1PageCategory>
                {
                    new M1PageCategory { Id = "1", Name = "Category1", DisplayOrder = 2, IsHeader = true },
                    new M1PageCategory { Id = "2", Name = "Category2", DisplayOrder = 1, IsHeader = true }
                };

            _mockRepository.Setup(repo => repo.GetHeaderCategoriesAsync())
                .ReturnsAsync(categories.OrderBy(c => c.DisplayOrder));

            // Act
            var result = await _service.GetHeaderCategoriesAsync();

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Equal("2", resultList[0].Id);
            Assert.Equal("1", resultList[1].Id);
        }
    }
}
