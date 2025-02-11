using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pokenae.Commons.Data;
using pokenae.WebSystem.API.DTOs;
using pokenae.WebSystem.API.Services;
using pokenae.WebSystem.Core.Entities;
using System.Security.Claims;

namespace pokenae.WebSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService)
        {
            _pageService = pageService;
        }

        /// <summary>
        /// 指定されたルートに対応するページを取得します。
        /// </summary>
        /// <param name="route">ページのルート</param>
        /// <returns>ページ情報</returns>
        [HttpGet("{route}")]
        public async Task<IActionResult> GetPageByRoute(string route)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            try
            {
                var page = await _pageService.GetPageByRouteAsync(route, userId);
                var pageDto = new PageDto
                {
                    Id = page.Id,
                    Title = page.Title,
                    Content = page.Content,
                    Route = page.Route
                };
                return Ok(pageDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
