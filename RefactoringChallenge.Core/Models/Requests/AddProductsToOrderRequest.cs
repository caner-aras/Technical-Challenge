using System.Collections.Generic;

namespace RefactoringChallenge.Core.Models.Requests
{
    public class AddProductsToOrderRequest
    {
        public int OrderId { get; set; }
        public IEnumerable<OrderDetailRequest> OrderDetails { get; set; }
    }
}
