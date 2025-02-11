using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.Core.Repositories;
using pokenae.WebSystem.Infrastructure.Data;
using pokenae.WebSystem.Infrastructure.Repositories;
using Xunit;

namespace pokenae.WebSystem.Tests.Repositories
{
    public class PageCategoryRepositoryTests
    {
        private readonly WebSystemDbContext _context;
        private readonly IPageCategoryRepository _repository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        public PageCategoryRepositoryTests()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();
            _mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(httpContext);

            var options = new DbContextOptionsBuilder<WebSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new WebSystemDbContext(options, _mockHttpContextAccessor.Object);
            _repository = new PageCategoryRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var categories = new List<M1PageCategory>
                {
                    new M1PageCategory { Id = "1", Name = "Category1", DisplayOrder = 2, IsHeader = true, CreatedBy = "system", CreatedProgramId = "test", UpdatedBy = "system", UpdatedProgramId = "test" },
                    new M1PageCategory { Id = "2", Name = "Category2", DisplayOrder = 1, IsHeader = true, CreatedBy = "system", CreatedProgramId = "test", UpdatedBy = "system", UpdatedProgramId = "test" },
                    new M1PageCategory { Id = "3", Name = "Category3", DisplayOrder = 3, IsHeader = false, CreatedBy = "system", CreatedProgramId = "test", UpdatedBy = "system", UpdatedProgramId = "test" }
                };

            _context.PageCategories.AddRange(categories);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetHeaderCategoriesAsync_ReturnsHeaderCategoriesInOrder()
        {
            // Act
            var result = await _repository.GetHeaderCategoriesAsync();

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Equal("2", resultList[0].Id);
            Assert.Equal("1", resultList[1].Id);
        }
    }
}
