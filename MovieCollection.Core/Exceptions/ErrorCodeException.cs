using MovieCollection.Core.Enums;
using MovieCollection.Core.Extensions;

namespace MovieCollection.Core.Exceptions
{
    public class ErrorCodeException : Exception
    {
        public ErrorCodeException(ErrorCodes errorCode) : base(errorCode.ToDescription())
        {
            ErrorCode = errorCode;
        }

        public ErrorCodeException(ErrorCodes errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ErrorCodes ErrorCode { get; }
    }
}
