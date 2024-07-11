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
}