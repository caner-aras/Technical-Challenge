using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefactoringChallenge.Core.Models.Requests;
using RefactoringChallenge.Core.Models.Responses;
using RefactoringChallenge.Core.Services.Interfaces;
using RefactoringChallenge.Models;
using System.Threading.Tasks;

namespace RefactoringChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(int? skip = null, int? take = null)
        {
            var response = new ListResponse<ProductResponse>(await _productService.GetAsync(skip, take));
            return Json(response);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int productId)
        {
            var response = new Response<ProductResponse>(await _productService.GetByIdAsync(productId));
            return Json(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAsync(AddProductRequest request)
        {
            var response = new Response<ProductResponse>(await _productService.CreateAsync(request));
            return Json(response);
        }

        [HttpPost("{productId}/[action]")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int productId)
        {
            var response = new Response<bool>(await _productService.DeleteAsync(productId));
            return Ok(response);
        }
    }
}
