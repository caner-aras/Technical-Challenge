using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RefactoringChallenge.Controllers;
using RefactoringChallenge.Core.Entities;
using RefactoringChallenge.Core.Models.Requests;
using RefactoringChallenge.Core.Models.Responses;
using RefactoringChallenge.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Refactoring.Test
{
    public class OrderServiceTest
    {
        [Fact]
        public void Can_Add_New_Order_Test()
        {
            // Arrange
            var _mockRepo = new Mock<IOrderService>();
            var _controller = new OrdersController(_mockRepo.Object);
            var _response = new OrderResponse();
            var _request = new CreateOrderRequest()
            {
                RequiredDate = DateTime.Now,
                CustomerId = "00001",
                ShipAddress = "7 NELSON LANE CV34 5JB",
                ShipCity = "WARWICK",
                ShipName = "CANER ARAS",
                ShipPostalCode = "CV34 5JB",
                ShipRegion = "WEST MIDLANDS",
                ShipCountry = "UK",
                OrderDetails = new List<OrderDetailRequest>()
                {
                    new OrderDetailRequest() {Discount = 0, Quantity= 1, UnitPrice = 100, ProductId=1 }
                },
                EmployeeId = 1
            };

            _mockRepo.Setup(repo => repo.CreateAsync(_request)).ReturnsAsync(_response);
            var response = _controller.CreateAsync(_request);

            // Assert

            var model = Assert.IsType<RefactoringChallenge.Models.Response<OrderResponse>>(((JsonResult)response.Result).Value);

            Assert.NotNull(model.Result);
        }
    }
}
