using Microsoft.AspNetCore.Http;

namespace pokenae.WebSystem.Infrastructure.impl
{
    /// <summary>
    /// HTTPコンテキストアクセサの実装
    /// </summary>
    public class HttpContextAccessor : IHttpContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext HttpContext
        {
            get => _httpContextAccessor.HttpContext;
            set => _httpContextAccessor.HttpContext = value;
        }
    }
}


