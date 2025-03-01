using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pokenae.Commons.Data;
using pokenae.WebSystem.API.DTOs;
using pokenae.WebSystem.API.Services;
using pokenae.WebSystem.Core.Entities;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pokenae.WebSystem.Application.Interfaces;
using Microsoft.Data.SqlClient;
using pokenae.WebSystem.API.Filters;

namespace pokenae.WebSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class PagesController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService)
        {
            _pageService = pageService;
        }

        /// <summary>
        /// �w�肳�ꂽ���[�g�Ɉ�v����y�[�W���擾���܂��B
        /// </summary>
        /// <param name="route">�y�[�W�̃��[�g</param>
        /// <returns>�y�[�W���</returns>
        [HttpGet("{route}")]
        public async Task<IActionResult> GetPageByRoute(string route)
        {
            if (User == null)
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

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
                    Id = page.NodeID,
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

        /// <summary>
        /// �w�b�_�[�y�[�W�̈ꗗ���擾���܂��B
        /// </summary>
        /// <returns>�w�b�_�[�y�[�W�̈ꗗ</returns>
        [HttpGet("headers")]
        public async Task<IActionResult> GetHeaderPages()
        {
            try
            {
                var pages = await _pageService.GetHeaderPagesAsync();
                var pageDtos = pages.Select(page => new PageDto
                {
                    Id = page.NodeID,
                    Title = page.Title,
                    Content = page.Content,
                    Route = page.Route
                }).ToList();

                return Ok(pageDtos);
            }
            catch (SqlException ex) when (ex.Number == -2)
            {
                // SQL Server connection timeout
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { message = "Database connection timeout. Please try again later." });
            }
            catch (Exception ex)
            {
                // General exception handling
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        /// <summary>
        /// �w�肳�ꂽ���[�g�ɑ΂���A�N�Z�X�����m�F���܂��B
        /// </summary>
        /// <param name="route">�y�[�W�̃��[�g</param>
        /// <returns>�A�N�Z�X���̊m�F����</returns>
        [HttpGet("check-access/{route}")]
        public async Task<IActionResult> CheckPageAccess(string route)
        {
            if (User == null)
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            var hasAccess = await _pageService.CheckPageAccessAsync(route, userId);
            if (!hasAccess)
            {
                return Forbid("User does not have access to this page.");
            }

            return Ok(new { message = "User has access to this page." });
        }
    }
}
