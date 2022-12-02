using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefactoringChallenge.Core.Entities;
using RefactoringChallenge.Core.Models.Requests;
using RefactoringChallenge.Core.Models.Responses;
using RefactoringChallenge.Core.Services.Interfaces;
using RefactoringChallenge.Models;
using System.Threading.Tasks;

namespace RefactoringChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoriesService;

        public CategoriesController(ICategoryService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(int? skip = null, int? take = null)
        {
            var response = new ListResponse<CategoryResponse>(await _categoriesService.GetAsync(skip, take));
            return Json(response);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int categoryId)
        {
            var response = new Response<CategoryResponse>(await _categoriesService.GetByIdAsync(categoryId));
            return Json(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAsync(AddCategoryRequest request)
        {
            var response = new Response<CategoryResponse>(await _categoriesService.CreateAsync(request));
            return Json(response);
        }

        [HttpPost("{categoryId}/[action]")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int categoryId)
        {
            var response = new Response<bool>(await _categoriesService.DeleteAsync(categoryId));
            return Ok(response);
        }
    }
}
