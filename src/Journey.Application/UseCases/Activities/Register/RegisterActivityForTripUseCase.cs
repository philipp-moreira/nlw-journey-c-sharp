using FluentValidation.Results;
using Journey.Communication.Enums;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.Messages;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Activities.Register;

public class RegisterActivityForTripUseCase
{
    public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson activity)
    {
        var repository = new JourneyDbContext();
        var tripEntity = repository.Trips.FirstOrDefault(activity => activity.Id.Equals(tripId));

        Validate(tripEntity, activity);

        var entityActivity = new Activity
        {
            Name = activity.Name,
            Date = activity.Date,
            TripId = tripId,
        };

        repository.Activities.Add(entityActivity);
        repository.SaveChanges();

        var response = new ResponseActivityJson
        {
            Id = entityActivity.Id,
            Name = entityActivity.Name,
            Status = (ActivityStatus)entityActivity.Status,
            Date = entityActivity.Date,
        };

        return response;
    }

    private void Validate(Trip? trip, RequestRegisterActivityJson activity)
    {
        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        var validator = new RegisterActivityValidator();
        var validationResult = validator.Validate(activity);

        if (!(activity.Date.Date >= trip.StartDate.Date && activity.Date.Date <= trip.EndDate.Date))
        {
            var failure = new ValidationFailure($"{nameof(RequestRegisterActivityJson.Date)}", ResourceErrorMessages.DATE_NOT_WITHIN_TRAVEL_PERIOD);
            validationResult.Errors.Add(failure);
        }

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
