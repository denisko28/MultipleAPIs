using Grpc.Core;
using GrpcAggregator.DTO.Response;
using GrpcAggregator.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace GrpcAggregator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomersService _customersService;

    public CustomerController(ICustomersService customersService)
    {
        _customersService = customersService;
    }

    // GET: api/Customer
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CustomerResponse>>> Get()
    {
        try
        {
            var results = await _customersService.GetAllAsync();
            return Ok(results);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
        }
    }

    // GET: api/Customer/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerResponse>> Get(int id)
    {
        try
        {
            var result = await _customersService.GetByIdAsync(id);
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