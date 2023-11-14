using Application.Commands.Employees;
using Application.Queries.Employees;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(EmployeeModel[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEmployees()
        {
            EmployeeModel[] employees = await _mediator.Send(new GetAllEmployees());
            return Ok(employees);
        }

        [HttpPost("GetEmployees")]
        [ProducesResponseType(typeof(EmployeeModel[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployees([FromBody] int[] employeeIds)
        {
            EmployeeModel[] employees = await _mediator.Send(new GetEmployees(employeeIds));
            return Ok(employees);
        }

        [HttpPost("SearchEmployees")]
        [ProducesResponseType(typeof(EmployeeModel[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchEmployees([FromBody] EmployeeSearchPayload payload)
        {
            EmployeeModel[] employees = await _mediator.Send(new SearchEmployees(payload));
            return Ok(employees);
        }

        [HttpPost("AddEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeePayload payload)
        {
            await _mediator.Send(new AddEmployee(payload));
            return Ok();
        }

        [HttpPost("Promote/{employeeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Promote([FromRoute] int employeeId)
        {
            await _mediator.Send(new Promote(employeeId));
            return Ok();
        }

        [HttpPost("UpdatePhone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePhone([FromBody] UpdatePhonePayload payload)
        {
            await _mediator.Send(new UpdatePhone(payload));
            return Ok();
        }

        [HttpPost("UpdateEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailPayload payload)
        {
            await _mediator.Send(new UpdateEmail(payload));
            return Ok();
        }

        [HttpPost("UpdateSalary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSalary([FromBody] UpdateSalaryPayload payload)
        {
            await _mediator.Send(new UpdateSalary(payload));
            return Ok();
        }

        [HttpDelete("DeleteEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEmployees([FromBody] int[] employeeIds)
        {
            await _mediator.Send(new DeleteEmployees(employeeIds));
            return Ok();
        }
    }
}
