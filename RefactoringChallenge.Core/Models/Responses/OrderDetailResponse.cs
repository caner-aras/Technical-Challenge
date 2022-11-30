using RefactoringChallenge.Core.Models.Interfaces;

namespace RefactoringChallenge.Core.Models.Responses
{
    public class OrderDetailResponse : IDTO
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
    }
}