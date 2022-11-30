using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RefactoringChallenge.Core.Models.Exceptions
{
    public class AppException : Exception
    {
        public AppException(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        public AppException(HttpStatusCode httpStatusCode, string appMessage)
        {
            HttpStatusCode = httpStatusCode;
            AppMessage = appMessage;
        }

        public HttpStatusCode HttpStatusCode { get; set; }
        public string AppMessage { get; set; }
    }
}
