namespace RefactoringChallenge.Models.Abstract
{
    public class BaseResponse
    {
        public string Code { get; set; } = "success";
        public string Message { get; set; } = "";
    }
}
