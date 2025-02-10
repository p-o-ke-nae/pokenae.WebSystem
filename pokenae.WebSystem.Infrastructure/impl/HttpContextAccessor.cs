using Microsoft.AspNetCore.Http;

namespace pokenae.WebSystem.Infrastructure.impl
{
    /// <summary>
    /// HTTP�R���e�L�X�g�A�N�Z�T�̎���
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


