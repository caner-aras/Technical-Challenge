using System;
using System.Collections.Generic;
using RefactoringChallenge.Core.Models.Interfaces;

namespace RefactoringChallenge.Core.Models.Responses
{
	public class ProductResponse : IDTO
    {
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public ICollection<CategoryResponse> Category { get; set; }

    }
}

