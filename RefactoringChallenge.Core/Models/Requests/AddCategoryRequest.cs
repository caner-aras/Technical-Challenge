using System;
using FluentValidation;

namespace RefactoringChallenge.Core.Models.Requests
{
	public class AddCategoryRequest
	{
		public string CategoryName { get; set; }
	}

    public class AddCategoryRequestValidator : AbstractValidator<AddCategoryRequest>
    {
        public AddCategoryRequestValidator()
        {
            RuleFor(x => x.CategoryName).NotNull().MinimumLength(5);
        }
    }
}

