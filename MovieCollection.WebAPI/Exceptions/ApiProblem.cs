using Microsoft.AspNetCore.Mvc;
using MovieCollection.Core.Enums;

namespace MovieCollection.WebAPI.Exceptions
{
    public class ApiProblem : ProblemDetails
    {
        public ApiProblem()
        {

        }

        public ApiProblem(int statusCode, string title, string details)
        {
            Status = statusCode;
            Title = title;
            Detail = details;
            ErrorCode = 0;

        }

        public ApiProblem(int statusCode, string title, string details, ErrorCodes errorCode) : this(statusCode, title, details)
        {
            Status = statusCode;
            Title = title;
            Detail = details;
            ErrorCode = (int)errorCode;
        }

        public int ErrorCode { get; }
    }
}
