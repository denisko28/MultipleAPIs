using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Services.Abstract;
using HR_DAL.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_API.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class BarberController : ControllerBase
    {
        private readonly IBarberService barberService;

        public BarberController(IBarberService barberService)
        {
            this.barberService = barberService;
        }

        // GET: api/Employee
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BarberResponse>>> Get()
        {
            try
            {
                IEnumerable<BarberResponse> results = await barberService.GetAllAsync();
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // GET: api/Employee/5
        [Microsoft.AspNetCore.Mvc.HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BarberResponse>> Get(int id)
        {
            try
            {
                BarberResponse result = await barberService.GetByIdAsync(id);
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

        // GET: api/Barber/BarbersAppointments/?barberId=4&date=2022-02-27
        [Microsoft.AspNetCore.Mvc.HttpGet("BarbersAppointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BarbersAppointmentsResponse>>> GetBarbersAppointments([FromUri] int barberId, [FromUri] string date)
        {
            try
            {
                var request = new BarbersAppointmentsRequest{ BarberId = barberId, _Date = date };
                IEnumerable<BarbersAppointmentsResponse> result = await barberService.GetBarbersAppointmentsAsync(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // POST: api/Employee
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([System.Web.Http.FromBody] BarberRequest request)
        {
            try
            {
                await barberService.InsertAsync(request);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // PUT: api/Employee
        [Microsoft.AspNetCore.Mvc.HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([System.Web.Http.FromBody] BarberRequest request)
        {
            try
            {
                await barberService.UpdateAsync(request);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // DELETE: api/Employee
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await barberService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
    }
}
