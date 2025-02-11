using Microsoft.EntityFrameworkCore;
using pokenae.WebSystem.Core.Entities;
using pokenae.WebSystem.Infrastructure.Repositories;
using pokenae.WebSystem.API.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace pokenae.WebSystem.API.Services.impl
{
    /// <summary>
    /// ページサービスの実装
    /// </summary>
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly AuthorizationService _authorizationService;

        public PageService(IPageRepository pageRepository, AuthorizationService authorizationService)
        {
            _pageRepository = pageRepository;
            _authorizationService = authorizationService;
        }

        public async Task<M1Page> GetPageByRouteAsync(string route, string userId)
        {
            if (!await _authorizationService.CheckUserAuthorizationAsync(userId, 1))
            {
                throw new UnauthorizedAccessException("User does not have sufficient permissions.");
            }

            var page = await _pageRepository.GetPageByRouteAsync(route);

            if (page == null)
            {
                throw new KeyNotFoundException("The page you are looking for does not exist.");
            }

            return page;
        }
    }
}

