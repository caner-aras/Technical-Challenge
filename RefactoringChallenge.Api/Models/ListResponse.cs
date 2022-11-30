using RefactoringChallenge.Models.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace RefactoringChallenge.Models
{
    public class ListResponse<T> : BaseResponse
    {
        public ListResponse(IEnumerable<T> items)
        {
            Result = items;
            Count = items.Count();
        }

        public IEnumerable<T> Result { get; set; }
        public int Count { get; set; }
    }
}
