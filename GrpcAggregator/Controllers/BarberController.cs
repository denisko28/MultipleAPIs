using Grpc.Core;
using GrpcAggregator.DTO.Response;
using GrpcAggregator.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace GrpcAggregator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarberController : ControllerBase
{
    private readonly IBarbersService _barbersService;

    public BarberController(IBarbersService barbersService)
    {
        _barbersService = barbersService;
    }

    // GET: api/Barber
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BarberResponse>>> Get()
    {
        try
        {
            var results = await _barbersService.GetAllAsync();
            return Ok(results);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new {e.Message});
        }
    }

    // GET: api/Barber/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BarberResponse>> Get(int id)
    {
        try
        {
            var result = await _barbersService.GetByIdAsync(id);
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
            return StatusCode(StatusCodes.Status500InternalServerError, new {e.Message});
        }
    }

    // GET: api/Barber/BarbersAppointments
    [HttpGet("BarbersAppointments/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CustomersAppointmentResponse>>> GetBarbersAppointments(int id)
    {
        try
        {
            var result = await _barbersService.GetBarbersAppointmentsAsync(id);
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
            return StatusCode(StatusCodes.Status500InternalServerError, new {e.Message});
        }
    }
}