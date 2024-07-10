using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception.Messages;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application;

public class RegisterTripUseCase
{
    public ResponseShortTripJson Execute(RequestRegisterTripJson trip)
    {
        Validate(trip);

        var repository = new JourneyDbContext();
        var entity = new Trip
        {
            Name = trip.Name,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate
        };

        repository.Trips.Add(entity);
        repository.SaveChanges();

        var response = new ResponseShortTripJson
        {
            Id = entity.Id,
            Name = entity.Name,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate
        };

        return response;
    }

    private void Validate(RequestRegisterTripJson trip)
    {
        if (string.IsNullOrWhiteSpace(trip.Name))
        {
            throw new JourneyException(ResourceErrorMessages.NAME_EMPTY);
        }

        if (trip.StartDate.Date < DateTime.UtcNow.Date)
        {
            throw new JourneyException(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);
        }

        if (trip.EndDate.Date < trip.StartDate.Date)
        {
            throw new JourneyException(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);
        }
    }
}
