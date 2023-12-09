using System.Net;

namespace MovieCollection.Core.Extensions
{
    internal class HttpStatusCodeAttribute : Attribute
    {
        public HttpStatusCodeAttribute(HttpStatusCode statusCode)
        {
            HttpStatusCode = statusCode;
        }

        public HttpStatusCode HttpStatusCode { get; }
    }
}
