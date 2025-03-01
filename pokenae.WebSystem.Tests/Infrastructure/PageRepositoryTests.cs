using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.Infrastructure.Data;
using pokenae.WebSystem.Infrastructure.Repositories;
using pokenae.WebSystem.Infrastructure.Repositories.impl;
using Xunit;

namespace pokenae.WebSystem.Tests.Infrastructure
{
    public class PageRepositoryTests
    {
        private readonly WebSystemDbContext _context;
        private readonly IPageRepository _repository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        public PageRepositoryTests()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();
            _mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(httpContext);

            var options = new DbContextOptionsBuilder<WebSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new WebSystemDbContext(options, _mockHttpContextAccessor.Object);
            _repository = new PageRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var pages = new List<M1Page>
                {
                    new M1Page { NodeID = "1", Title = "Page1", DisplayOrder = 2, Content = "Page1test", Route = "route1", IsHeader = true, PageState = PageStates.Published, CreatedBy = "system", CreatedProgramId = "test", UpdatedBy = "system", UpdatedProgramId = "test" },
                    new M1Page { NodeID = "2", Title = "Page2", DisplayOrder = 1, Content = "Page2test", Route = "route2", IsHeader = true, PageState = PageStates.Published, CreatedBy = "system", CreatedProgramId = "test", UpdatedBy = "system", UpdatedProgramId = "test" },
                    new M1Page { NodeID = "3", Title = "Page3", DisplayOrder = 3, Content = "Page3test", Route = "route3", IsHeader = false, PageState = PageStates.Unpublished, CreatedBy = "system", CreatedProgramId = "test", UpdatedBy = "system", UpdatedProgramId = "test" }
                };

            _context.Pages.AddRange(pages);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetHeaderPagesAsync_ReturnsHeaderPagesInOrder()
        {
            // Act
            var result = await _repository.GetHeaderPagesAsync();

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Equal("2", resultList[0].NodeID);
            Assert.Equal("1", resultList[1].NodeID);
        }

        [Fact]
        public async Task CheckPageAccessAsync_ReturnsTrue_ForPublishedPage()
        {
            // Act
            var result = await _repository.CheckPageAccessAsync("route1", "user1");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckPageAccessAsync_ReturnsFalse_ForUnpublishedPage()
        {
            // Act
            var result = await _repository.CheckPageAccessAsync("route3", "user1");

            // Assert
            Assert.False(result);
        }
    }
}
