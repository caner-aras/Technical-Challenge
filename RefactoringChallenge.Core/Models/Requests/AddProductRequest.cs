using System;
using FluentValidation;

namespace RefactoringChallenge.Core.Models.Requests
{
	public class AddProductRequest
	{
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
    {
        public AddProductRequestValidator()
        {
            RuleFor(x => x.CategoryId).NotEqual(0);
            RuleFor(x => x.ProductName).NotNull().MinimumLength(5);
            RuleFor(x => x.UnitPrice).NotEqual(0);
        }
    }
}

