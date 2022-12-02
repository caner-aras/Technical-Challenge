using FluentValidation;
using Mapster;
using RefactoringChallenge.Core.Entities;
using RefactoringChallenge.Core.Models.Exceptions;
using RefactoringChallenge.Core.Models.Requests;
using RefactoringChallenge.Core.Models.Responses;
using RefactoringChallenge.Core.Repositories.Interfaces;
using RefactoringChallenge.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RefactoringChallenge.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category, CategoryResponse> _categoryRepository;

        public CategoryService(
            IRepository<Category, CategoryResponse> categoryRepository
            )
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> CreateAsync(AddCategoryRequest request)
        {
            return (await _categoryRepository.AddAsync(request.Adapt<Category>())).Adapt<CategoryResponse>();
        }

        public async Task<bool> DeleteAsync(int categoryId)
        {
            var product = await _categoryRepository.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
            if (product == null)
                throw new AppException(HttpStatusCode.NotFound, "Category not found!");


            return await _categoryRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<CategoryResponse>> GetAsync(int? skip = null, int? take = null)
        {
            var query = _categoryRepository.AsQueryable();
            if (skip != null)
            {
                query = query.Skip(skip.Value);
            }
            if (take != null)
            {
                query = query.Take(take.Value);
            }
            return await _categoryRepository.GetAllAsDtoAsync(query);
        }

        public async Task<CategoryResponse> GetByIdAsync(int categoryId)
        {
            return await _categoryRepository.FirstOrDefaultAsDtoAsync(x => x.CategoryId == categoryId);
        }
    }
}
