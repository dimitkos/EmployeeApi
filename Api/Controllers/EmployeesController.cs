using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        //private readonly IMediator _mediator;

        //public EmployeesController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        [HttpGet]
        [ProducesResponseType(typeof(EmployeeModel[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEmployees()
        {
            EmployeeModel[] employees = await _mediator.Send(new GetAllEmployees());
            return Ok(employees);
        }

        [HttpPost("GetEmployees")]
        [ProducesResponseType(typeof(EmployeeModel[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployees([FromBody] long[] employeeIds)
        {
            EmployeeModel[] employees = await _mediator.Send(new GetEmployees(employeeIds));
            return Ok(employees);
        }

        [HttpPost("AddEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeePayload payload)
        {
            await _mediator.Send(new AddStudent(payload));
            return Ok();
        }

        [HttpPost("Promote/{employeeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Promote([FromRoute] int employeeId)
        {
            //await _mediator.Send(new RemoveStudent(Promote));
            //return Ok();
        }

        [HttpPost("UpdatePhone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePhone([FromBody] UpdatePhonePayload payload)
        {
            //await _mediator.Send(new UpdateStudentPhone(payload));
            //return Ok();
        }

        [HttpPost("UpdateEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailPayload payload)
        {
            //await _mediator.Send(new UpdateStudentPhone(payload));
            //return Ok();
        }

        [HttpPost("UpdateSalary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSalary([FromBody] UpdateSalaryPayload payload)
        {
            //await _mediator.Send(new UpdateStudentPhone(payload));
            //return Ok();
        }
    }
}
