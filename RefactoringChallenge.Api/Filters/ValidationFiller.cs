using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace RefactoringChallenge.Filters
{
    public class ValidationFiller : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                throw new ValidationException(context.ModelState.Select(
                    x => new FluentValidation.Results.ValidationFailure(x.Key, string.Join(";", x.Value.Errors.Select(y => y.ErrorMessage)))));
            }

            await next();
        }
    }
}
