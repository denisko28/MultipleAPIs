using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services_Application.Commands.ServiceDiscounts.DeleteServiceDiscount;
using Services_Application.Commands.ServiceDiscounts.InsertServiceDiscount;
using Services_Application.Commands.ServiceDiscounts.UpdateServiceDiscount;
using Services_Application.DTO.Requests;
using Services_Application.DTO.Responses;
using Services_Application.Exceptions;
using Services_Application.Queries.ServiceDiscounts.GetAllServiceDiscounts;
using Services_Application.Queries.ServiceDiscounts.GetByIdServiceDiscount;

namespace Services_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceDiscountController : ControllerBase
    {
        private readonly IMediator mediator;

        public ServiceDiscountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/ServiceDiscount
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ServiceDiscountResponse>>> Get()
        {
            try
            {
                var results = await mediator.Send(new GetAllServiceDiscountsQuery());
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // GET: api/ServiceDiscount/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ServiceDiscountResponse>> Get(int id)
        {
            try
            {
                var result = await mediator.Send(new GetByIdServiceDiscountQuery{ Id = id});
                return Ok(result);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // POST: api/ServiceDiscount
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] ServiceDiscountRequest request)
        {
            try
            {
                await mediator.Send(new InsertServiceDiscountCommand(){ ServiceDiscountRequest = request });
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // PUT: api/ServiceDiscount
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] ServiceDiscountRequest request)
        {
            try
            {
                await mediator.Send(new UpdateServiceDiscountCommand{ ServiceDiscountRequest = request});
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // DELETE: api/ServiceDiscount
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await mediator.Send(new DeleteServiceDiscountCommand{ Id = id });
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
    }
}
