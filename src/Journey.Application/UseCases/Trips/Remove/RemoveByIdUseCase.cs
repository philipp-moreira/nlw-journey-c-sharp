using Journey.Exception;
using Journey.Exception.Messages;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Trips.Remove;

public class RemoveByIdUseCase
{
    public void Execute(Guid id)
    {
        var repository = new JourneyDbContext();

        var entity = repository.Trips.FirstOrDefault(trip => trip.Id.Equals(id));

        if (entity is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        repository.Trips.Remove(entity);
        repository.SaveChanges();
    }
}
