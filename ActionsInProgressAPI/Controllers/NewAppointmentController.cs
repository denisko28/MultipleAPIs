using ActionsInProgressAPI.Entities;
using ActionsInProgressAPI.Exceptions;
using ActionsInProgressAPI.Repositories.Abstract;
using AutoMapper;
using Common.Events.AppointmentEvents;
using IdentityServer.Helpers;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActionsInProgressAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewAppointmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        
        private readonly IUnfinishedAppointmentRepository _unfinishedAppointmentRepository;
        
        private readonly IPublishEndpoint _publishEndpoint;

        public NewAppointmentController(IMapper mapper, IUnfinishedAppointmentRepository unfinishedAppointmentRepository, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _unfinishedAppointmentRepository = unfinishedAppointmentRepository;
            _publishEndpoint = publishEndpoint;
        }
        
        // GET: api/NewAppointment/5
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Customer)]
        [HttpGet("{customerUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UnfinishedAppointment?>> GetUnfinishedAppointment(int customerUserId)
        {
            try
            {
                var result = await _unfinishedAppointmentRepository.GetAsync(customerUserId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        //POST: api/NewAppointment/5
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Customer)]
        [HttpPost("{customerUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddUnfinishedAppointment(int customerUserId)
        {
            try
            {
                var initialUnfinishedAppointment = new UnfinishedAppointment{ CustomerUserId = customerUserId };
                await _unfinishedAppointmentRepository.InsertAsync(initialUnfinishedAppointment);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // PUT: api/NewAppointment
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Customer)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateUnfinishedAppointment([FromBody] UnfinishedAppointment unfinishedAppointment)
        {
            try
            {
                await _unfinishedAppointmentRepository.UpdateAsync(unfinishedAppointment);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // DELETE: api/NewAppointment
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Customer)]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUnfinishedAppointment(int id)
        {
            try
            {
                await _unfinishedAppointmentRepository.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // POST: api/NewAppointment/FinishAppointment
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Customer)]
        [HttpPost("FinishAppointment/{customerUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> FinishAppointment(int customerUserId)
        {
            try
            {
                var unfinishedAppointment = await _unfinishedAppointmentRepository.GetAsync(customerUserId);
                if (unfinishedAppointment == null)
                    throw new UnfinishedAppointmentsNotFoundException(customerUserId);

                // send finished appointment event to rabbitmq
                var eventMessage = _mapper.Map<FinishedAppointmentEvent>(unfinishedAppointment);
                await _publishEndpoint.Publish(eventMessage);
                
                await _unfinishedAppointmentRepository.DeleteByIdAsync(customerUserId);
                return Ok();
            }
            catch (UnfinishedAppointmentsNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new {e.Message});
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {e.Message});
            }
        }
    }
}
