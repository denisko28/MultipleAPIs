using Grpc.Core;
using GrpcAggregator.DTO.Response;
using GrpcAggregator.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace GrpcAggregator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvailableTimeController : ControllerBase
{
    private readonly IAvailableTimeService _availableTimeService;

    public AvailableTimeController(IAvailableTimeService availableTimeService)
    {
        _availableTimeService = availableTimeService;
    }

    // GET: api/AvailableTime/GetAvailableTime/?barberId=4&duration=15&date=2022-03-08
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TimeResponse>>> Get([FromQuery] int barberId, [FromQuery] int duration, [FromQuery] string date)
    {
        try
        {
            var result = await _availableTimeService.GetAvailableTimeAsync(barberId, duration, date);
            return Ok(result);
        }
        catch (RpcException e)
        {
            return StatusCode(
                e.Status.StatusCode == Grpc.Core.StatusCode.NotFound
                    ? StatusCodes.Status404NotFound
                    : StatusCodes.Status500InternalServerError, new {e.Message});
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
        }
    }
}