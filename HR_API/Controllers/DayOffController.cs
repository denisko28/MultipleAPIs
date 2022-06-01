using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Services.Abstract;
using HR_DAL.Exceptions;
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DayOffResponse>>> Get()
        {
            try
            {
                var results = await dayOffService.GetAllCompleteEntities();
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // GET: api/DayOff/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DayOffResponse>> Get(int id)
        {
            try
            {
                var result = await dayOffService.GetCompleteEntity(id);
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
        
        // GET: api/EmployeeDayOff/GetByEmployee/14
        [HttpGet("GetByEmployee/{employeeUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DayOffResponse>>> GetByEmployee(int employeeUserId)
        {
            try
            {
                var results = await dayOffService.GetDayOffsByEmployee(employeeUserId);
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
        
        // GET: api/EmployeeDayOff/GetByDate/2022-03-27
        [HttpGet("GetByDate/{dateStr}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DayOffResponse>>> GetByDate(string dateStr)
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] DayOffPostRequest request)
        {
            try
            {
                await dayOffService.InsertAsync(request);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // PUT: api/DayOff
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] DayOffRequest request)
        {
            try
            {
                await dayOffService.UpdateAsync(request);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // DELETE: api/DayOff
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
    }
}
