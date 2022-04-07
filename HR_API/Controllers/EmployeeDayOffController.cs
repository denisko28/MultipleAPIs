using Microsoft.AspNetCore.Mvc;
using MultipleAPIs.HR_BLL.DTO.Requests;
using MultipleAPIs.HR_BLL.DTO.Responses;
using MultipleAPIs.HR_DAL.Exceptions;
using MultipleAPIs.HR_BLL.Services.Abstract;

namespace HR_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeDayOffController : ControllerBase
    {
        private readonly IEmployeeDayOffService employeeDayOffService;

        public EmployeeDayOffController(IEmployeeDayOffService employeeDayOffService)
        {
            this.employeeDayOffService = employeeDayOffService;
        }

        // GET: api/Employee
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EmployeeDayOffResponse>>> Get()
        {
            try
            {
                var results = await employeeDayOffService.GetAllAsync();
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDayOffResponse>> Get(int id)
        {
            try
            {
                var result = await employeeDayOffService.GetByIdAsync(id);
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

        // POST: api/Employee
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] EmployeeDayOffRequest request)
        {
            try
            {
                await employeeDayOffService.InsertAsync(request);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // PUT: api/Employee
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] EmployeeDayOffRequest request)
        {
            try
            {
                await employeeDayOffService.UpdateAsync(request);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // DELETE: api/Employee
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                await employeeDayOffService.DeleteByIdAsync(Id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
    }
}
