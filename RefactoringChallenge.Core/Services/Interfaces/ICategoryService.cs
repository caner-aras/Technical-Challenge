using RefactoringChallenge.Core.Models.Requests;
using RefactoringChallenge.Core.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactoringChallenge.Core.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAsync(int? skip = null, int? take = null);
        Task<CategoryResponse> GetByIdAsync(int categoryId);
        Task<CategoryResponse> CreateAsync(AddCategoryRequest request);
        Task<bool> DeleteAsync(int categoryId);
    }
}
