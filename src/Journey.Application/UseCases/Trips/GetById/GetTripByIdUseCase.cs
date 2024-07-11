using Journey.Communication.Enums;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.Messages;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById;

public class GetTripByIdUseCase
{
    public ResponseTripJson Execute(Guid id)
    {
        var repository = new JourneyDbContext();

        var entity = repository
                        .Trips
                        .Include(trip => trip.Activities)
                        .FirstOrDefault(trip => trip.Id.Equals(id));

        if (entity is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        var activities = entity.Activities.Select(activity => new ResponseActivityJson
        {
            Id = activity.Id,
            Name = activity.Name,
            Status = (ActivityStatus)activity.Status,
            Date = activity.Date
        }).ToList();

        var response = new ResponseTripJson
        {
            Id = entity.Id,
            Name = entity.Name,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Activities = activities,
        };

        return response;
    }
}
