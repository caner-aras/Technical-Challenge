using RefactoringChallenge.Core.Models.Requests;
using RefactoringChallenge.Core.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactoringChallenge.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetAsync(int? skip = null, int? take = null);
        Task<OrderResponse> GetByIdAsync(int orderId);
        Task<OrderResponse> CreateAsync(CreateOrderRequest request);
        Task<IEnumerable<OrderDetailResponse>> AddProductsToOrderAsync(AddProductsToOrderRequest request);
        Task<bool> DeleteAsync(int orderId);
    }
}
