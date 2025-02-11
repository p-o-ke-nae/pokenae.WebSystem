using Microsoft.AspNetCore.Mvc;
using pokenae.WebSystem.API.Services;
using System.Threading.Tasks;

namespace pokenae.WebSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageCategoryController : ControllerBase
    {
        private readonly IPageCategoryService _service;

        public PageCategoryController(IPageCategoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// �w�b�_�ɕ\������J�e�S���ꗗ���擾���܂�
        /// </summary>
        [HttpGet("header-categories")]
        public async Task<IActionResult> GetHeaderCategories()
        {
            var categories = await _service.GetHeaderCategoriesAsync();
            return Ok(categories);
        }
    }
}
