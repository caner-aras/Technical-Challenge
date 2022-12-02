using System;
using RefactoringChallenge.Core.Models.Interfaces;

namespace RefactoringChallenge.Core.Models.Responses
{
	public class CategoryResponse : IDTO
    {
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
	}
}

