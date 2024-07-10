using Journey.Communication.Responses;
using Journey.Infrastructure;

namespace Journey.Application;

public class GetAllTripUseCase
{
    public ResponseTripsJson Execute()
    {
        var repository = new JourneyDbContext();

        var entities = repository.Trips;

        var response = new ResponseTripsJson
        {
            Trips = entities.Select(entity =>
                new ResponseShortTripJson
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate
                }).ToList(),
        };

        return response;
    }
}
