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
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order, OrderResponse> _orderRepository;
        private readonly IRepository<OrderDetail, OrderDetailResponse> _orderDetailRepository;

        public OrderService(
            IRepository<Order, OrderResponse> orderRepository,
            IRepository<OrderDetail, OrderDetailResponse> orderDetailRepository
            )
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<IEnumerable<OrderDetailResponse>> AddProductsToOrderAsync(AddProductsToOrderRequest request)
        {
            var order = await _orderRepository.FirstOrDefaultAsync(o => o.OrderId == request.OrderId);
            if (order == null)
                throw new AppException(HttpStatusCode.NotFound, "Order not found!");

            var newOrderDetails = request.OrderDetails.Adapt<IEnumerable<OrderDetail>>().ToList();
            newOrderDetails.ForEach(x => x.OrderId = request.OrderId);

            return (await _orderDetailRepository.AddRangeAsync(newOrderDetails)).Adapt<IEnumerable<OrderDetailResponse>>();
        }

        public async Task<OrderResponse> CreateAsync(CreateOrderRequest request)
        {
            ///HACK: I could have used this option as well
            //await new CreateOrderRequestValidator().ValidateAndThrowAsync(request);

            var newOrder = request.Adapt<Order>();
            newOrder.OrderDate = DateTime.Now;

            return (await _orderRepository.AddAsync(newOrder)).Adapt<OrderResponse>();
        }

        public async Task<bool> DeleteAsync(int orderId)
        {
            var order = await _orderRepository.FirstOrDefaultAsync(x => x.OrderId == orderId);
            if (order == null)
                throw new AppException(HttpStatusCode.NotFound, "Order not found!");

            var orderDetails = await _orderDetailRepository.GetAllAsync(od => od.OrderId == orderId);

            await _orderDetailRepository.DeleteRangeAsync(orderDetails);
            return await _orderRepository.DeleteAsync(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetAsync(int? skip = null, int? take = null)
        {
            var query = _orderRepository.AsQueryable();
            if (skip != null)
            {
                query = query.Skip(skip.Value);
            }
            if (take != null)
            {
                query = query.Take(take.Value);
            }
            return await _orderRepository.GetAllAsDtoAsync(query);
        }

        public async Task<OrderResponse> GetByIdAsync(int orderId)
        {
            return await _orderRepository.FirstOrDefaultAsDtoAsync(x => x.OrderId == orderId);
        }
    }
}
