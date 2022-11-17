using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services_Application.Commands.Services.DeleteService;
using Services_Application.Commands.Services.InsertService;
using Services_Application.Commands.Services.UpdateService;
using Services_Application.DTO.Requests;
using Services_Application.DTO.Responses;
using Services_Application.Exceptions;
using Services_Application.Queries.Services.GetAllServices;
using Services_Application.Queries.Services.GetByIdService;

namespace Services_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator mediator;

        public ServiceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/Service
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ServiceResponse>>> Get()
        {
            try
            {
                var results = await mediator.Send(new GetAllServicesQuery());
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
        public async Task<ActionResult<ServiceResponse>> Get(int id)
        {
            try
            {
                var result = await mediator.Send(new GetByIdServiceQuery{ Id = id});
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
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] ServicePostRequest request)
        {
            try
            {
                await mediator.Send(new InsertServiceCommand{ ServiceRequest = request });
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // PUT: api/ServiceDiscount
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] ServiceRequest request)
        {
            try
            {
                await mediator.Send(new UpdateServiceCommand{ ServiceRequest = request});
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
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await mediator.Send(new DeleteServiceCommand{ Id = id });
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
