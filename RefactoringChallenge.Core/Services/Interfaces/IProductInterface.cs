using RefactoringChallenge.Core.Models.Requests;
using RefactoringChallenge.Core.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactoringChallenge.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAsync(int? skip = null, int? take = null);
        Task<ProductResponse> GetByIdAsync(int productId);
        Task<ProductResponse> CreateAsync(AddProductRequest request);
        Task<bool> DeleteAsync(int productId);
    }
}
