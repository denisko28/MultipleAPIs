using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Exceptions;
using Customers_BLL.Helpers;
using Customers_BLL.Services.Abstract;
using Customers_DAL.Exceptions;
using Customers_DAL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customers_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        // GET: api/Appointment
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AppointmentResponse>>> Get()
        {
            try
            {
                IEnumerable<AppointmentResponse> results = await appointmentService.GetAllAsync();
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // GET: api/appointment/ForManager
        [Authorize(Roles = UserRoles.Manager)]
        [HttpGet("ForManager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AppointmentResponse>>> GetForManager()
        {
            try
            {
                var userId = UserClaimsHelper.GetUserId(HttpContext);
                IEnumerable<AppointmentResponse> results = await appointmentService.GetAllForManager(userId);
                return Ok(results);
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

        // GET: api/Appointment/5
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppointmentResponse>> Get(int id)
        {
            try
            {
                AppointmentResponse result = await appointmentService.GetByIdAsync(id);
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
        
        // GET: api/appointment/ForManager/3
        [Authorize(Roles = UserRoles.Manager)]
        [HttpGet("ForManager/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppointmentResponse>> GetForManager(int id)
        {
            try
            {
                var userId = UserClaimsHelper.GetUserId(HttpContext);
                AppointmentResponse result = await appointmentService.GetByIdForManager(id, userId);
                return Ok(result);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (ForbiddenAccessException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // GET: api/Appointment/GetByDate/
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetByDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AppointmentResponse>>> GetByDate([FromQuery] string date)
        {
            try
            {
                IEnumerable<AppointmentResponse> results = await appointmentService.GetByDateAsync(date);
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // GET: api/Appointment/GetServices/4
        [Authorize]
        [HttpGet("GetServices/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ServiceResponse>>> GetServices(int id)
        {
            try
            {
                IEnumerable<ServiceResponse> results = await appointmentService.GetAppointmentServicesAsync(id);
                return Ok(results);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new {e.Message});
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {e.Message});
            }
        }

        // GET: api/Appointment/GetAvailableTime/?barberId=4&duration=15&date=2022-03-08
        [Authorize]
        [HttpGet("GetAvailableTime")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TimeResponse>>> GetAvailableTime([FromQuery] int barberId, [FromQuery] int duration, [FromQuery] string date)
        {
            try
            {
                var result = await appointmentService.GetAvailableTimeAsync(barberId, duration, date);
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

        // POST: api/Appointment
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] AppointmentPostRequest request)
        {
            try
            {
                await appointmentService.InsertAsync(request);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // PUT: api/Appointment
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] AppointmentRequest request)
        {
            try
            {
                await appointmentService.UpdateAsync(request);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // PUT: api/Appointment/ForManager
        [Authorize(Roles = UserRoles.Manager)]
        [HttpPut("ForManager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutForManager([FromBody] AppointmentRequest request)
        {
            try
            {
                var userId = UserClaimsHelper.GetUserId(HttpContext);
                await appointmentService.UpdateForManagerAsync(request, userId);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new {e.Message});
            }
            catch (ForbiddenAccessException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // PUT: api/Appointment/ForBarber
        [Authorize(Roles = UserRoles.Manager)]
        [HttpPut("ForBarber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutForBarber([FromBody] AppointmentRequest request)
        {
            try
            {
                var userId = UserClaimsHelper.GetUserId(HttpContext);
                await appointmentService.UpdateForBarberAsync(request, userId);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new {e.Message});
            }
            catch (ForbiddenAccessException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // DELETE: api/Appointment
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await appointmentService.DeleteByIdAsync(id);
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
