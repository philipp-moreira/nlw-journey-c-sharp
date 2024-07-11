namespace Journey.Communication.Responses;

public class ResponseErrorsJson
{
    public IList<string> Errors { get; init; }

    public ResponseErrorsJson(IList<string> errors)
    {
        Errors = errors;
    }
}
