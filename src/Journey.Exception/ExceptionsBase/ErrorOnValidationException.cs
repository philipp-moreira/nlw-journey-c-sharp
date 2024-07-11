using System.Net;
using Journey.Exception.ExceptionsBase;

namespace Journey.Exception;

public class ErrorOnValidationException : JourneyException
{
    private readonly IList<string> _errors = [];

    public ErrorOnValidationException(IList<string> messages)
        : base(string.Empty)
    {
        _errors = messages;
    }

    public override HttpStatusCode GetHttpErrorCode() => HttpStatusCode.BadRequest;
    public override IList<string> GetErrorMessages() => _errors;
}
