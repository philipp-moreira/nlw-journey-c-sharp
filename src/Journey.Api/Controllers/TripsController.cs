using Journey.Application;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly string GENERAL_ERROR_MESSAGE = "Unexpected error.";

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var useCase = new GetAllTripUseCase();
            var response = useCase.Execute();

            return Ok(response);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, GENERAL_ERROR_MESSAGE);
        }
    }

    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterTripJson request)
    {
        try
        {
            var useCase = new RegisterTripUseCase();
            var result = useCase.Execute(request);

            return Created(string.Empty, result);
        }
        catch (JourneyException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, GENERAL_ERROR_MESSAGE);
        }
    }
}