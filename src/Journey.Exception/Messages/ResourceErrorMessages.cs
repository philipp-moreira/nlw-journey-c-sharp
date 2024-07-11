namespace Journey.Exception.Messages;

public static class ResourceErrorMessages
{
    public static string GENERAL_ERROR { get; } = "Unexpected error.";
    public static string NAME_EMPTY { get; } = "The name can´t be empty.";
    public static string DATE_TRIP_MUST_BE_LATER_THAN_TODAY { get; } = "The date of the trip must be later than today.";
    public static string END_DATE_TRIP_MUST_BE_LATER_START_DATE { get; } = "The end date of the drip must be equal or later than the start date.";
    public static string TRIP_NOT_FOUND { get; } = "Trip not found.";
}