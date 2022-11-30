using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RefactoringChallenge.Core.Models.Exceptions;
using RefactoringChallenge.Models.Abstract;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace RefactoringChallenge.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode httpStatusCode;
            string code;
            string message;

            switch (ex)
            {
                case AppException appException:
                    httpStatusCode = appException.HttpStatusCode;
                    code = "error";
                    message = appException.AppMessage;
                    break;
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    code = "error_400";
                    message = $"{string.Join(" ", validationException.Errors.Select(x => x.ErrorMessage))}";
                    break;
                default:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    code = "error_500";
                    message = ex.Message;
                    break;
            }

            var response = new BaseResponse()
            {
                Code = code,
                Message = message
            };

            string result = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(result);

        }
    }
}
