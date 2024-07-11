using Journey.Exception;
using Journey.Exception.Messages;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Activities.Remove;

public class DeleteActivityForTripUseCase
{
    public void Execute(Guid tripId, Guid activityId)
    {
        var repository = new JourneyDbContext();
        var entityActivity = repository
                                .Activities
                                .FirstOrDefault(activity => activity.Id.Equals(activityId) && activity.TripId.Equals(tripId));

        if (entityActivity is null)
        {
            throw new NotFoundException(ResourceErrorMessages.ACTIVITY_NOT_FOUND);
        }

        repository.Activities.Remove(entityActivity);
        repository.SaveChanges();   
    }
}