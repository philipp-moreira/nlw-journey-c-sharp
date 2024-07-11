using Journey.Exception;
using Journey.Exception.Messages;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;

namespace Journey.Application.UseCases.Activities.Complete;

public class CompleteActivityForTripUseCase
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

        entityActivity.Status = ActivityStatus.Done;

        repository.Activities.Update(entityActivity);
        repository.SaveChanges();
    }
}
