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
    public class ProductService: IProductService
    {
        private readonly IRepository<Product, ProductResponse> _productRepository;

        public ProductService(
            IRepository<Product, ProductResponse> productRepository
            )
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> CreateAsync(AddProductRequest request)
        {
            return (await _productRepository.AddAsync(request.Adapt<Product>())).Adapt<ProductResponse>();
        }

        public async Task<bool> DeleteAsync(int productId)
        {
            var product = await _productRepository.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (product == null)
                throw new AppException(HttpStatusCode.NotFound, "Product not found!");

            var orderDetails = await _productRepository.GetAllAsync(od => od.ProductId == productId);

            return await _productRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<ProductResponse>> GetAsync(int? skip = null, int? take = null)
        {
            var query = _productRepository.AsQueryable();
            if (skip != null)
            {
                query = query.Skip(skip.Value);
            }
            if (take != null)
            {
                query = query.Take(take.Value);
            }
            return await _productRepository.GetAllAsDtoAsync(query);
        }

        public async Task<ProductResponse> GetByIdAsync(int productId)
        {
            return await _productRepository.FirstOrDefaultAsDtoAsync(x => x.ProductId == productId);
        }
    }
}
