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
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(int? skip = null, int? take = null)
        {
            var response = new ListResponse<OrderResponse>(await _orderService.GetAsync(skip, take));
            return Json(response);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int orderId)
        {
            var response = new Response<OrderResponse>(await _orderService.GetByIdAsync(orderId));
            return Json(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAsync(CreateOrderRequest request)
        {
            var response = new Response<OrderResponse>(await _orderService.CreateAsync(request));
            return Json(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProductsToOrderAsync(AddProductsToOrderRequest request)
        {
            var response = new ListResponse<OrderDetailResponse>(await _orderService.AddProductsToOrderAsync(request));
            return Json(response);
        }

        [HttpPost("{orderId}/[action]")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int orderId)
        {
            var response = new Response<bool>(await _orderService.DeleteAsync(orderId));
            return Ok(response);
        }
    }
}
