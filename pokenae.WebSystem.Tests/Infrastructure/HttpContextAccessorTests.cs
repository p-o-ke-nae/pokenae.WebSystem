using Microsoft.AspNetCore.Http;
using Moq;
using pokenae.WebSystem.Infrastructure.impl;
using Xunit;

namespace pokenae.WebSystem.Tests.Infrastructure
{
    public class HttpContextAccessorTests
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly pokenae.WebSystem.Infrastructure.impl.HttpContextAccessor _accessor;

        public HttpContextAccessorTests()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _accessor = new pokenae.WebSystem.Infrastructure.impl.HttpContextAccessor(_mockHttpContextAccessor.Object);
        }

        [Fact]
        public void HttpContext_ReturnsHttpContext()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext).Returns(mockHttpContext.Object);

            // Act
            var result = _accessor.HttpContext;

            // Assert
            Assert.Equal(mockHttpContext.Object, result);
        }
    }
}
