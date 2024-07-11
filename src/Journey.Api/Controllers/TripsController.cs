using Journey.Application.UseCases.Activities.Complete;
using Journey.Application.UseCases.Activities.Register;
using Journey.Application.UseCases.Activities.Remove;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Application.UseCases.Trips.Remove;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    #region Trips
    [HttpGet]
    [ProducesResponseType<ResponseTripsJson>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        var useCase = new GetAllTripUseCase();
        var response = useCase.Execute();

        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType<ResponseTripJson>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status500InternalServerError)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var useCase = new GetTripByIdUseCase();
        var response = useCase.Execute(id);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType<ResponseTripJson>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status500InternalServerError)]
    public IActionResult Register([FromBody] RequestRegisterTripJson request)
    {
        var useCase = new RegisterTripUseCase();
        var result = useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status500InternalServerError)]
    public IActionResult Remove([FromRoute] Guid id)
    {
        var useCase = new RemoveByIdUseCase();
        useCase.Execute(id);

        return NoContent();
    }
    #endregion

    #region Activities

    [HttpPost]
    [Route("{tripId}/activity")]
    [ProducesResponseType<ResponseActivityJson>(StatusCodes.Status201Created)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status500InternalServerError)]
    public IActionResult RegisterActivity([FromRoute] Guid tripId, [FromBody] RequestRegisterActivityJson request)
    {
        var useCase = new RegisterActivityForTripUseCase();
        var result = useCase.Execute(tripId, request);

        return Created(string.Empty, result);
    }

    [HttpPut]
    [Route("{tripId}/activity/{activityId}/complete")]
    [ProducesResponseType<ResponseActivityJson>(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status500InternalServerError)]
    public IActionResult CompleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
    {
        var useCase = new CompleteActivityForTripUseCase();
        useCase.Execute(tripId, activityId);

        return NoContent();
    }

    [HttpDelete]
    [Route("{tripId}/activity/{activityId}")]
    [ProducesResponseType<ResponseActivityJson>(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ResponseErrorsJson>(StatusCodes.Status500InternalServerError)]
    public IActionResult RemoveActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
    {
        var useCase = new DeleteActivityForTripUseCase();
        useCase.Execute(tripId, activityId);

        return NoContent();
    }
    #endregion
}