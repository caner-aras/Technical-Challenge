using RefactoringChallenge.Models.Abstract;

namespace RefactoringChallenge.Models
{
    public class Response<T> : BaseResponse
    {
        public Response(T item)
        {
            Result = item;
        }

        public T Result { get; }
    }
}
