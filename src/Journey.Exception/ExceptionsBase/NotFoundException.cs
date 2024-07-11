using System.Net;
using Journey.Exception.ExceptionsBase;

namespace Journey.Exception;

public class NotFoundException : JourneyException
{
    public NotFoundException(string message)
        : base(message)
    {
    }

    public override HttpStatusCode GetHttpErrorCode() => HttpStatusCode.NotFound;
    public override IList<string> GetErrorMessages() => [Message];
}
