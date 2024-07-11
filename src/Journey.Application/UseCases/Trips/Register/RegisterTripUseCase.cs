using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register;

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
        var validator = new RegisterTripValidator();
        var validationResult = validator.Validate(trip);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
