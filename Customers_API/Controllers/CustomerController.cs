using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Exceptions;
using Customers_BLL.Services.Abstract;
using Customers_DAL.Exceptions;
using IdentityServer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customers_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customer
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> Get()
        {
            try
            {
                var results = await _customerService.GetAllAsync();
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // GET: api/Customer/5
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerResponse>> Get(int id)
        {
            try
            {
                var result = await _customerService.GetByIdAsync(id);
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
        
        // GET: api/Customer/CustomersAppointments
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Customer)]
        [HttpGet("CustomersAppointments/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CustomersAppointmentResponse>>> GetCustomersAppointments(int id)
        {
            try
            {
                var userClaims = UserClaimsHelper.GetUserClaims(HttpContext);
                var result =
                    await _customerService.GetCustomersAppointments(id, userClaims);
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

        // POST: api/Customer
        // [HttpPost]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // public async Task<ActionResult> Post([FromBody] CustomerRequest request)
        // {
        //     try
        //     {
        //         await customerService.InsertAsync(request);
        //         return Ok();
        //     }
        //     catch (Exception e)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
        //     }
        // }

        // PUT: api/Customer
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Customer)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] CustomerRequest request)
        {
            try
            {
                var userClaims = UserClaimsHelper.GetUserClaims(HttpContext);
                await _customerService.UpdateAsync(request, userClaims);
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

        // DELETE: api/Customer
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _customerService.DeleteByIdAsync(id);
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
