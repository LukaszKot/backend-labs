using System.Net;

namespace OnTheTrail.IdentityService.Api.Exceptions;

public class AppException : Exception
{
    public HttpStatusCode StatusCode { get;  }

    public AppException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, Exception? innerException = null) : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}