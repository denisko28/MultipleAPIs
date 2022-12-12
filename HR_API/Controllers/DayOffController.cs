using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Exceptions;
using HR_BLL.Services.Abstract;
using HR_DAL.Exceptions;
using IdentityServer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DayOffController : ControllerBase
    {
        private readonly IDayOffService dayOffService;
        
        public DayOffController(IDayOffService dayOffService)
        {
            this.dayOffService = dayOffService;
        }

        // GET: api/DayOff
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DayOffResponseDto>>> Get()
        {
            try
            {
                var results = await dayOffService.GetAllAsync();
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // GET: api/DayOff
        [Authorize(Roles = UserRoles.Manager)]
        [HttpGet("ForManager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DayOffResponseDto>>> GetForManager()
        {
            try
            {
                var userId = UserClaimsHelper.GetUserId(HttpContext);
                var results = await dayOffService.GetAllForManager(userId);
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

        // GET: api/DayOff/5
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DayOffResponseDto>> Get(int id)
        {
            try
            {
                var result = await dayOffService.GetByIdAsync(id);
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
        
        // GET: api/DayOff/5
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("ForManager/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DayOffResponseDto>> GetForManager(int id)
        {
            try
            {
                var userId = UserClaimsHelper.GetUserId(HttpContext);
                var result = await dayOffService.GetByIdForManager(id, userId);
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
        
        // GET: api/EmployeeDayOff/GetByEmployee/14
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Barber)]
        [HttpGet("GetByEmployee/{employeeUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DayOffResponseDto>>> GetByEmployee(int employeeUserId)
        {
            try
            {
                var userClaims = UserClaimsHelper.GetUserClaims(HttpContext);
                var results = await dayOffService.GetDayOffsByEmployee(employeeUserId, userClaims);
                return Ok(results);
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
        
        // GET: api/EmployeeDayOff/GetByEmployee/14
        [Authorize(Roles = UserRoles.Manager)]
        [HttpGet("GetByEmployee/ForManager/{employeeUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DayOffResponseDto>>> GetByEmployeeForManager(int employeeUserId)
        {
            try
            {
                var userId = UserClaimsHelper.GetUserId(HttpContext);
                var results = 
                    await dayOffService.GetDayOffsByEmployeeForManager(employeeUserId, userId);
                return Ok(results);
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
        
        // GET: api/EmployeeDayOff/GetByDate/2022-03-27
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetByDate/{dateStr}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DayOffResponseDto>>> GetByDate(string dateStr)
        {
            try
            {
                var date = DateTime.Parse(dateStr);
                var results = await dayOffService.GetCompleteEntitiesByDate(date);
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // POST: api/DayOff
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] DayOffPostRequestDto requestDto)
        {
            try
            {
                await dayOffService.InsertAsync(requestDto);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // POST: api/DayOff
        [Authorize(Roles = UserRoles.Manager)]
        [HttpPost("ForManager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostForManager([FromBody] DayOffPostRequestDto requestDto)
        {
            try
            {
                var userId = UserClaimsHelper.GetUserId(HttpContext);
                await dayOffService.InsertForManagerAsync(requestDto, userId);
                return Ok();
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

        // PUT: api/DayOff
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] DayOffRequestDto requestDto)
        {
            try
            {
                await dayOffService.UpdateAsync(requestDto);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        
        // PUT: api/DayOff
        [Authorize(Roles = UserRoles.Manager)]
        [HttpPut("ForManager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutForManager([FromBody] DayOffRequestDto requestDto)
        {
            try
            {
                var userId = UserClaimsHelper.GetUserId(HttpContext);
                await dayOffService.UpdateForManager(requestDto, userId);
                return Ok();
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

        // DELETE: api/DayOff
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await dayOffService.DeleteByIdAsync(id);
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
        
        // DELETE: api/DayOff
        [Authorize(Roles = UserRoles.Manager)]
        [HttpDelete("ForManager/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteForManager(int id)
        {
            try
            {
                var userId = UserClaimsHelper.GetUserId(HttpContext);
                await dayOffService.DeleteByIdForManager(id, userId);
                return Ok();
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
    }
}
